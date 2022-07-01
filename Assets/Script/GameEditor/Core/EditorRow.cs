using UnityEngine;

namespace GameEditor.Core
{
    public class EditorRow : MonoBehaviour
    {
        [SerializeField]
        private EditorCell[] _cells;

        public EditorCell[] Cells => _cells;
    }
}