using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CommonFramework.Core.Exception
{
    public partial class TechnicalException: System.Exception, ISerializable
    {

        public TechnicalException()
        {
            throw new System.Exception(CommonFramework.Resources.ExceptionThrownFromCommnonFrameWork);
        }


        public TechnicalException(String expMessage)
        {
            throw new System.Exception(expMessage);
        }


        public TechnicalException(String expMessage, System.Exception InnerException)
        {
            throw new System.Exception(expMessage, InnerException);
        }


        protected TechnicalException(SerializationInfo info, StreamingContext context)
        {
            ///TODO
        }

    }

}
