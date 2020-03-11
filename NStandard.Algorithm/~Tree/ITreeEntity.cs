using System;

namespace NStandard.Algorithm
{
    public interface ITreeEntity
    {
        Guid Id { get; }
        long Index { get; }
        Guid? Parent { get; }
    }

}
