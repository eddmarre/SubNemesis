using System;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace SubNemesis.GamePlay
{

    public class FindFeels : MonoBehaviour
    {
       private MMFeedback mmFeedbackDamage;
       private MMF_Player mmFeedbackDash;

       private void Awake()
       {
           mmFeedbackDamage = FindObjectOfType<CameraShake>().GetComponent<MMFeedback>();
           mmFeedbackDash = FindObjectOfType<DashEffect>().GetComponent<MMF_Player>();
       }

       public void PlayDashFeel()
       {
           mmFeedbackDash.PlayFeedbacks();
       }

       public void PlayDamageFeel()
       {
           mmFeedbackDamage.Play(transform.position);
       }
    }
}