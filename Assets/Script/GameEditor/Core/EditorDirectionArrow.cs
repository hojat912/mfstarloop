using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

namespace GameEditor.Core
{
    public class EditorDirectionArrow : MonoBehaviour
    {
        [SerializeField]
        private Image[] _images;

        [SerializeField]
        private CellVector _moveDirection;

        public CellVector MoveDirection => _moveDirection;

        private void Awake()
        {
            int length = _images.Length;
            Color color = new Color(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), 0.75f);

            for (int i = 0; i < length; i++)
            {
                _images[i].color = color;
            }
        }


        public void SetMoveDirection(CellVector direction)
        {
            _moveDirection = direction;
            if (direction == CellVector.Down)
            {
                transform.eulerAngles = new Vector3(0, 0, -90);

            }
            else if (direction == CellVector.Up)
            {
                transform.eulerAngles = new Vector3(0, 0, 90);

            }
            else if (direction == CellVector.Right)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);

            }
            else if (direction == CellVector.Left)
            {
                transform.eulerAngles = new Vector3(0, 0, 180);
            }
        }

    }
}