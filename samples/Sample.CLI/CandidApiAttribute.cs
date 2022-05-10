
namespace ICP.Sample.CLI
{
    public class CandidApiAttribute : Attribute
    {
        public string DIDFilePath { get; }
        public CandidApiAttribute(string didFilePath)
        {
            this.DIDFilePath = didFilePath;
        }

    }
}