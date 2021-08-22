using System;
using System.Collections.Generic;
using System.Linq;

namespace AFetter.Battleship.Domain
{
    public class ResponseEnvelope<T>
    {
        public ResponseEnvelope() : this(default(T))
        {}

        public ResponseEnvelope(T result) : this(result, new List<string>())
        {}

        public ResponseEnvelope(T result, IList<string> errors)
        {
            Result = result;
            Errors = errors;
        }

        public T Result { get; set; }

        public IList<string> Errors { get; set; }

        public bool HasError
        {
            get
            {
                return Errors != null && Errors.Any();
            }
        }

        public ResponseEnvelope<T> AddError(string error)
        {
            Errors.Add(error);
            return this;
        }
    }
}
