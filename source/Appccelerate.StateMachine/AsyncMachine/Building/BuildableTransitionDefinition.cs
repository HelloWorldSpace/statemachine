// <copyright file="BuildableTransitionDefinition.cs" company="Appccelerate">
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

namespace Appccelerate.StateMachine.AsyncMachine.Building
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Appccelerate.StateMachine.AsyncMachine.ActionHolders;
    using Appccelerate.StateMachine.AsyncMachine.GuardHolders;
    using Appccelerate.StateMachine.AsyncMachine.Transitions;

    public class BuildableTransitionDefinition<TState, TEvent>
        where TState : notnull
        where TEvent : notnull
    {
        public BuildableTransitionDefinition(TEvent @event)
        {
            this.Event = @event;
        }

        public BuildableStateDefinition<TState, TEvent>? Source { get; set; }

        public TEvent Event { get; set; }

        public BuildableStateDefinition<TState, TEvent>? Target { get; set; }

        public IGuardHolder? Guard { get; set; }

        public ICollection<IActionHolder> Actions { get; } = new List<IActionHolder>();

        public bool IsInternalTransition => this.Target == null;

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "Transition from state {0} to state {1}.", this.Source, this.Target);
        }
    }
}