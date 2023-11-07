using System;
using System.Collections.Generic;

namespace Main.Scripts.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {

        private readonly Dictionary<Type, IExitableState> _states = new();
        private IExitableState _activeState;

        public void AddState(IExitableState state)
        {
            _states[state.GetType()] = state;
            state.StateMachine = this;
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TParameter>(TParameter payload) where TState : class, IParametrizedState<TParameter>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
      
            TState state = GetState<TState>();
            _activeState = state;
      
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}