using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField] AudioClip shoot;
	[SerializeField] AudioClip gotHit;
	[SerializeField] AudioClip truckDrive;

    private static AudioClip StaticShoot;
    private static AudioClip StaticGotHit;
    private static AudioClip StaticTruckDrive;

    void Start()
    {
        StaticShoot = shoot;
        StaticGotHit = gotHit;
        StaticTruckDrive = truckDrive;
    }

	public static AudioClip Shoot{
		get{
			return StaticShoot;
		}
	}
	public static AudioClip GotHit{
		get{
			return StaticGotHit;
		}
	}
	public static AudioClip TruckDrive{
		get{
			return StaticTruckDrive;
		}
	}
}
