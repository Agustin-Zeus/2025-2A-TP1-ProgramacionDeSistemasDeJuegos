using System.Collections.Generic;
using UnityEngine;

namespace Excercise1
{
    public class CharacterService : MonoBehaviour
    {
        public static CharacterService Instance {  get; private set; }
        private readonly Dictionary<string, ICharacter> _charactersById = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public bool TryAddCharacter(string id, ICharacter character)
            => _charactersById.TryAdd(id, character);
        public bool TryRemoveCharacter(string id)
            => _charactersById.Remove(id);

        public bool TryGetCharacter(string id, out ICharacter character)
            => _charactersById.TryGetValue(id, out character);

    }
}
