using System;
using System.Threading.Tasks;
using Dahomey.Cbor.ObjectModel;

namespace ICP.Agent.Responses
{
    public class ReadStateResponse
    {
        public byte[] Certificate { get; }

        public ReadStateResponse(byte[] certificate)
        {
            this.Certificate = certificate ?? throw new ArgumentNullException(nameof(certificate));
        }

        public static ReadStateResponse FromCbor(CborObject cbor)
        {
            byte[] certificate = cbor["certificate"].Value<byte[]>();

            return new ReadStateResponse(certificate);
        }
    }
}