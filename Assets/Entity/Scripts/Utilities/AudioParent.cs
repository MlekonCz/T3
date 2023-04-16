using UnityEngine;

namespace Entity.Scripts.Utilities
{
   public class AudioParent : MonoBehaviour
   {
      [SerializeField] private AudioSource _AudioDay;
      [SerializeField] private AudioSource _AudioNight;


      [SerializeField] private AudioClip _AudioClipDay;
      [SerializeField] private AudioClip _AudioClipNight;
   }
}
