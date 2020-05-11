using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace DD2
{
    public class ManaOrb : MonoBehaviour
    {
        [SerializeField] int amount;
        [SerializeField] float pickUpSpeed;
        [SerializeField] float terminateDistance;

        Rigidbody rb;
        bool isPickedUp;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        void OnEnable()
        {
            rb.isKinematic = false;
            isPickedUp = false;
        }

        public void PickUp(Entity entity)
        {
            if (!isPickedUp)
            {
                Timing.RunCoroutine(PickUpRoutine(entity));
                rb.isKinematic = true;
                isPickedUp = true;
            }            
        }

        public void Burst(float strength)
        {
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.2f, 0.2f), Random.Range(-1f, 1f));
            rb.AddForce(direction.normalized * Random.Range(0f, strength), ForceMode.Impulse);
        }

        IEnumerator<float> PickUpRoutine(Entity entity)
        {
            while (true)
            {
                if (entity.CurrentMana < entity.Stats.MaxMana)
                {
                    Vector3 direction = Vector3.Normalize(entity.GetPosition() - transform.position);
                    transform.position += direction * pickUpSpeed * Time.deltaTime;
                    if (Vector3.Distance(transform.position, entity.GetPosition()) < terminateDistance)
                    {
                        entity.GiveMana(amount);
                        ManaOrbPool.Instance.ReturnObject(amount.ToString(), this);
                        break;
                    }
                }
                else
                {
                    rb.isKinematic = false;
                    rb.velocity = Vector3.Normalize(entity.GetPosition() - transform.position) * pickUpSpeed;
                    isPickedUp = false;
                    break;
                }                
                yield return Timing.WaitForOneFrame;
            }
        }

        public void SetIsPickedUp(bool value)
        {
            isPickedUp = value;
        }

        public Rigidbody GetRigidbody()
        {
            return rb;
        }

        public int GetAmount()
        {
            return amount;
        }
    }
}