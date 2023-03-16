using System.Collections.Generic;
using System;
using UnityEngine;

namespace IdleRPG.Components
{
    public class PoolMono<T> where T : MonoBehaviour
    {
        public T prefab { get; }
        public bool autoExpend { get; set; }
        public Transform countainer { get; }

        private List<T> pool;

        public PoolMono(T prefab, int count)
        {
            this.prefab = prefab;
            this.countainer = null;

            this.CreatePool(count);
        }

        public PoolMono(T prefab, int count, Transform countainer)
        {
            this.prefab = prefab;
            this.countainer = countainer;

            this.CreatePool(count);
        }


        private void CreatePool(int count)
        {
            this.pool = new List<T>();

            for (int i = 0; i < count; i++)
            {
                CreateObject();
            }
        }

        private T CreateObject(bool isActiveByDefault = false)
        {
            var createdObject = UnityEngine.Object.Instantiate(this.prefab, this.countainer);
            createdObject.gameObject.SetActive(isActiveByDefault);
            this.pool.Add(createdObject);

            return createdObject;
        }

        public bool HasFreeElement(out T element)
        {
            foreach (var mono in pool)
            {
                if (!mono.gameObject.activeInHierarchy)
                {
                    element = mono;
                    mono.gameObject.SetActive(true);
                    return true;
                }
            }

            element = null;
            return false;
        }
        public bool HasFreeElement(out T element, Transform transform)
        {
            foreach (var mono in pool)
            {
                if (!mono.gameObject.activeInHierarchy)
                {
                    element = mono;
                    mono.transform.position = transform.position;
                    mono.gameObject.SetActive(true);
                    return true;
                }
            }

            element = null;
            return false;
        }

        public T GetFreeElement()
        {
            if (this.HasFreeElement(out var element))
                return element;

            if (this.autoExpend)
                return this.CreateObject(true);

            throw new Exception($"There is no free elements in pool of type {typeof(T)}");
        }

        public T GetFreeElement(Transform transform)
        {
            if (this.HasFreeElement(out var element, transform))
            {
                element.transform.position = transform.position;
                return element;
            }

            if (this.autoExpend)
                return this.CreateObject(true);

            throw new Exception($"There is no free elements in pool of type {typeof(T)}");
        }
    }
}
