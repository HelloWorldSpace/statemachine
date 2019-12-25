//-------------------------------------------------------------------------------
// <copyright file="ITransitionDefinition.cs" company="Appccelerate">
//   Copyright (c) 2008-2019 Appccelerate
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace Appccelerate.StateMachine.AsyncMachine.Transitions
{
    using System;
    using System.Collections.Generic;
    using ActionHolders;
    using GuardHolders;
    using States;

    public interface ITransitionDefinition<TState, TEvent>
        where TState : notnull
        where TEvent : notnull
    {
        IStateDefinition<TState, TEvent> Source { get; }

        TEvent Event { get; }

        IStateDefinition<TState, TEvent>? Target { get; }

        IGuardHolder? Guard { get; }

        IEnumerable<IActionHolder> Actions { get; }

        bool IsInternalTransition { get; }
    }
}