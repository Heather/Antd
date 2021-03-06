﻿using System;

namespace antdlib.Websocket.Exceptions {
    public class EntityTooLargeException : Exception {
        /// <summary>
        /// Http header too large to fit in buffer
        /// </summary>
        public EntityTooLargeException(string message)
            : base(message) {
        }
    }
}
