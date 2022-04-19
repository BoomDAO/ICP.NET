using Dahomey.Cbor;
using Dahomey.Cbor.ObjectModel;
using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using ICP.Agent;
using ICP.Agent.Auth;
using ICP.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agent.Cbor
{
	internal class ContentCborConverter : ICborConverter
	{
        public void Write(ref CborWriter writer, object value)
        {
            var actualValue = (Dictionary<string, IHashable>)value;
            actualValue.ToHashable()
        }

        object? ICborConverter.Read(ref CborReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
