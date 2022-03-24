using Huntress.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Huntress.Characters
{
    public class ArrowController : MonoBehaviour
    {
        [SerializeField] float speed;
        private void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        }
    }
}
