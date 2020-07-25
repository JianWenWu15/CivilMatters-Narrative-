namespace Ilumisoft.VisualStateMachine
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    [DefaultExecutionOrder(-1)]
    [DisallowMultipleComponent]
    public class StateMachine : MonoBehaviour
    {
        [SerializeField]
        private Graph graph = new Graph();

        /// <summary>
        /// Returns the ID of the currently active state or string.Empty if none is active
        /// </summary>
        public string CurrentState { get; protected set; } = string.Empty;

        /// <summary>
        /// Gets  the graph of the state machine, which contains all nodes and transitions
        /// </summary>
        public Graph Graph => graph;

        /// <summary>
        /// Gets the ID of the entry state
        /// </summary>
        public string EntryState => this.graph.EntryStateID;

        public void Start()
        {
            // Don't reset state machine, if it has already been started.
            // When the state machine is created at runtime, this method might get called multiple times
            // or from anopther script
            if (CurrentState == string.Empty)
            {
                Restart();
            }
        }

        private void Update()
        {
            if(CurrentState != string.Empty && graph.TryGetState(CurrentState, out State state))
            {
                state?.OnUpdateState?.Invoke();
            }
        }

        /// <summary>
        /// Restarts the state machine by entering the entry state.
        /// Remark: The OnExitState event of the currently active state will not be triggered, when calling this method!
        /// </summary>
        public void Restart()
        {
            this.CurrentState = this.EntryState;

            if (this.graph.TryGetNode(this.CurrentState, out Node node))
            {
                if (node is State state)
                {
                    state.OnEnterState.Invoke();
                }
            }
            else
            {
                Debug.LogWarning("Could not start state machine, because no entry state has been set", this);
            }
        }

        /// <summary>
        /// Tries to trigger the first transition found, which has the given label and goes out from the currently active state. 
        /// Returns true on success, false if none could be found.
        /// </summary>
        /// <param name="transitionLabel"></param>
        /// <returns></returns>
        public bool TryTriggerByLabel(string transitionLabel)
        {
            // Check if there is any transition with the given label and the current state as its origin
            foreach (var transition in graph.Transitions)
            {
                if (transition.Label == transitionLabel && transition.OriginID == CurrentState)
                {
                    Trigger(transition);
                    return true;
                }
            }

            // Check if there is any transition with the given label and any state as its origin
            foreach (var transition in graph.Transitions)
            {
                if (transition.Label == transitionLabel)
                {
                    if (graph.TryGetNode(transition.OriginID, out Node node) && node is AnyState)
                    {
                        Trigger(transition);
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Triggers the first transition found, which has the given label and goes out from the currently active state.
        /// </summary>
        /// <param name="transitionLabel"></param>
        public void TriggerByLabel(string transitionLabel)
        {
            if (TryTriggerByLabel(transitionLabel) == false)
            {
                Debug.LogWarning($"There is no transition with label {transitionLabel}, which could be triggered in the current context", this);
            }
        }

        /// <summary>
        /// Tries to trigger the transition with the given ID and returns true on success, false otherwise.
        /// </summary>
        /// <param name="transitionID"></param>
        /// <returns></returns>
        public bool TryTrigger(string transitionID)
        {
            bool success = false;

            if (graph.TryGetTransition(transitionID, out Transition transition))
            {
                if (this.graph.TryGetNode(transition.OriginID, out Node originNode))
                {
                    if (originNode is State state && state.ID == this.CurrentState)
                    {
                        success = true;
                    }
                    else if (originNode is AnyState anyState)
                    {
                        success = true;
                    }
                }
            }

            if(success)
            {
                Trigger(transition);
            }

            return success;
        }

        /// <summary>
        /// Triggers the transition with the given id, if it exists and if its origin state is the current state or an any state
        /// </summary>
        /// <param name="transitionID"></param>
        public void Trigger(string transitionID)
        {
            if (TryTrigger(transitionID) == false)
            {
                if (graph.TryGetTransition(transitionID, out Transition transition))
                {
                    if (transition.OriginID != CurrentState)
                    {
                        Debug.LogWarningFormat("Failed to trigger transition with id {0}, because the current state is not its origin", transitionID);
                    }
                }
                else
                {
                    Debug.LogWarningFormat("Failed to trigger transition with id {0}, because no transition with this id exists", transitionID);
                }
            }
        }

        /// <summary>
        /// Triggers the given transition
        /// </summary>
        /// <param name="transition"></param>
        public void Trigger(Transition transition)
        {
            if(transition == null)
            {
                Debug.LogWarningFormat("Failed to trigger the given transition, because it is null");
                return;
            }

            if(transition!= null && graph.HasTransition(transition.ID) == false)
            {
                Debug.LogWarningFormat("Failed to trigger transition with name {0}, because no transition with this id exists", transition.ID);
                return;
            }

            if (this.graph.TryGetNode(transition.OriginID, out Node originNode))
            {
                if (originNode is State state && state.ID == this.CurrentState)
                {
                    state.OnExitState.Invoke();
                }
                else if (originNode is AnyState anyState)
                {
                    if (this.graph.TryGetNode(this.CurrentState, out Node activeState))
                    {
                        if (activeState is State)
                        {
                            ((State)activeState).OnExitState.Invoke();
                        }
                    }
                }
                else
                {

                    Debug.LogErrorFormat("Failed to trigger transition with name {0}, because its origin is not the current state", transition.ID, this);

                    return;
                }
            }

            transition.OnEnterTransition.Invoke();
            transition.OnExitTransition.Invoke();

            if (this.graph.TryGetNode(transition.TargetID, out Node targetNode))
            {
                if (targetNode is State state)
                {
                    state.OnEnterState.Invoke();

                    this.CurrentState = state.ID;
                }
                else if (targetNode is AnyState anyState)
                {
                    Debug.LogErrorFormat("Target node of a transition cannot be an AnyState");
                }
            }
        }

        #region Obsolete

        /// <summary>
        /// Returns true if the graph contains a state with the given id, false otherwise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Obsolete("This method is obsolete. Call Graph.HasState instead.")]
        public bool HasState(string id) => graph.HasState(id);

        /// <summary>
        /// return true if the graph contains a transition with the given id, false otherwise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Obsolete("This method is obsolete. Call Graph.HasTransition instead.")]
        public bool HasTransition(string id) => graph.HasTransition(id);

        /// <summary>
        /// Returns the OnEnterState event of the state with the given name, null if the state does not exist 
        /// </summary>
        /// <param name="stateName"></param>
        /// <returns></returns>
        [Obsolete("This method is obsolete. Call Graph.GetState instead.")]
        public UnityEvent GetOnEnterStateEvent(string stateName)
        {
            if (graph.TryGetNode(stateName, out Node node))
            {
                if (node is State state)
                {
                    return state.OnEnterState;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the OnExitState event of the state with the given name, null if the state does not exist 
        /// </summary>
        /// <param name="stateName"></param>
        /// <returns></returns>
        [Obsolete("This method is obsolete. Call Graph.GetState instead.")]
        public UnityEvent GetOnExitStateEvent(string stateName)
        {
            if (graph.TryGetNode(stateName, out Node node))
            {
                if (node is State state)
                {
                    return state.OnExitState;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the OnEnterTransition event of the transition with the given name, null if the transition does not exist 
        /// </summary>
        /// <param name="transitionName"></param>
        /// <returns></returns>
        [Obsolete("This method is obsolete. Call Graph.GetTransition instead.")]
        public UnityEvent GetOnEnterTransitionEvent(string transitionName)
        {
            if (graph.TryGetTransition(transitionName, out Transition transition))
            {
                return transition.OnEnterTransition;
            }

            return null;
        }

        /// <summary>
        /// Returns the OnExitTransition event of the transition with the given name, null if the transition does not exist 
        /// </summary>
        /// <param name="transitionName"></param>
        /// <returns></returns>
        [Obsolete("This method is obsolete. Call Graph.GetTransition instead.")]
        public UnityEvent GetOnExitTransitionEvent(string transitionName)
        {
            if (graph.TryGetTransition(transitionName, out Transition transition))
            {
                return transition.OnExitTransition;
            }

            return null;
        }
        #endregion
    }
}