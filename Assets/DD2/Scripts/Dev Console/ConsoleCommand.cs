using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DD2
{
    public class ConsoleCommandBase
    {
        public string Id { get; protected set; }
        public string Description { get; protected set; }
        public string Format { get; protected set; }
    }

    public class ConsoleCommand : ConsoleCommandBase
    {
        private Action action;

        public ConsoleCommand(string id, string description, string format, Action command)
        {
            Id = id;
            Description = description;
            Format = format;
            action = command;
        }

        public void Invoke()
        {
            action.Invoke();
        }
    }

    public class ConsoleCommand<T1> : ConsoleCommandBase
    {
        private Action<T1> action;

        public ConsoleCommand(string id, string description, string format, Action<T1> command)
        {
            Id = id;
            Description = description;
            Format = format;
            action = command;
        }

        public void Invoke(T1 t1)
        {
            action.Invoke(t1);
        }
    }
}
