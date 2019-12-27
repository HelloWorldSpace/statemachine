//-------------------------------------------------------------------------------
// <copyright file="TransitionContext.cs" company="Appccelerate">
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
    using System.Diagnostics;
    using System.Text;
    using Appccelerate.StateMachine.AsyncMachine.States;

    /// <summary>
    /// Provides context information during a transition.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    [DebuggerDisplay("State = {StateDefinition} Event = {EventId} EventArgument = {EventArgument}")]
    public class TransitionContext<TState, TEvent> : ITransitionContext<TState, TEvent>
        where TState : notnull
        where TEvent : notnull
    {
        private readonly List<Record> records;

        public TransitionContext(
            IStateDefinition<TState, TEvent>? stateDefinition,
            Option<TEvent> eventId,
            object? eventArgument,
            INotifier<TState, TEvent> notifier)
        {
            this.StateDefinition = stateDefinition;
            this.EventId = eventId;
            this.EventArgument = eventArgument;
            this.Notifier = notifier;

            this.records = new List<Record>();
        }

        public IStateDefinition<TState, TEvent>? StateDefinition { get; }

        public Option<TEvent> EventId { get; }

        public object? EventArgument { get; }

        private INotifier<TState, TEvent> Notifier { get; }

        public void OnExceptionThrown(Exception exception)
        {
            this.Notifier.OnExceptionThrown(this, exception);
        }

        public void OnTransitionBegin()
        {
            this.Notifier.OnTransitionBegin(this);
        }

        public void AddRecord(TState stateId, RecordType recordType)
        {
            this.records.Add(new Record(stateId, recordType));
        }

        public string GetRecords()
        {
            var result = new StringBuilder();

            this.records.ForEach(record => result.AppendFormat(" -> {0}", record));

            return result.ToString();
        }

        private class Record
        {
            public Record(TState stateId, RecordType recordType)
            {
                this.StateId = stateId;
                this.RecordType = recordType;
            }

            private TState StateId { get; }

            private RecordType RecordType { get; }

            public override string ToString()
            {
                return this.RecordType + " " + this.StateId;
            }
        }
    }
}