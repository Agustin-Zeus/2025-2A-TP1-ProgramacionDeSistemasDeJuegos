using UnityEngine;

namespace Excercise1
{
    public interface ICharacter
    {
        Transform transform { get; }
        string Id {  get; }
    }
}