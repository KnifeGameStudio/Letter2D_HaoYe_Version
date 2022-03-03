using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioSource audioSrc;
    public static AudioClip openDrawer;
    public static AudioClip openDoor;
    public static AudioClip LockedDoor;
    public static AudioClip Healing;
    public static AudioClip writeHW;
    public static AudioClip HWMonsDown;
    public static AudioClip ManHurt;
    public static AudioClip Computer_Windows;
    public static AudioClip magic;
    public static AudioClip girl;
    public static AudioClip bossDefeat;
    public static AudioClip hitHWMons;
    public static AudioClip hwmonsComing;
    public static AudioClip puzzleSolved;
    public static AudioClip teacherMonscoming;
    public static AudioClip dizzy;
    public static AudioClip peer;
    public static AudioClip scaredBreath;
    public static AudioClip question;
    public static AudioClip tired;
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        openDrawer = Resources.Load<AudioClip>("Drawer");
        openDoor = Resources.Load<AudioClip>("Door");
        LockedDoor = Resources.Load<AudioClip>("LockedDoor");
        Healing = Resources.Load<AudioClip>("Heal");
        writeHW = Resources.Load<AudioClip>("Write");
        HWMonsDown = Resources.Load<AudioClip>("HomeworkMonsDown");
        ManHurt = Resources.Load<AudioClip>("Man_Hurt");
        Computer_Windows = Resources.Load<AudioClip>("Windows");
        magic = Resources.Load<AudioClip>("Magic");
        girl = Resources.Load<AudioClip>("Girl");
        bossDefeat = Resources.Load<AudioClip>("Boss_Defeat");
        hitHWMons = Resources.Load<AudioClip>("Hit_HWMons");
        hwmonsComing = Resources.Load<AudioClip>("HWMons_Coming");
        puzzleSolved = Resources.Load<AudioClip>("Puzzle_Solve");
        teacherMonscoming = Resources.Load<AudioClip>("TeacherMons_Coming");
        dizzy = Resources.Load<AudioClip>("Dizzy");
        peer = Resources.Load<AudioClip>("Peer");
        scaredBreath = Resources.Load<AudioClip>("Scared_Breath");
        question = Resources.Load<AudioClip>("Question");
        tired = Resources.Load<AudioClip>("Tired");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void playDrawerClip()
    {
        audioSrc.PlayOneShot(openDrawer);
    }

    public static void playDoorClip()
    {
        audioSrc.PlayOneShot(openDoor);
    }

    public static void playLockedDoorClip()
    {
        audioSrc.PlayOneShot(LockedDoor);
    }

    public static void playHealClip()
    {
        audioSrc.PlayOneShot(Healing);
    }

    public static void playWriteClip()
    {
        audioSrc.PlayOneShot(writeHW);
    }

    public static void playHWMonsDownClip()
    {
        audioSrc.PlayOneShot(HWMonsDown);
    }

    public static void playManHurtClip()
    {
        audioSrc.PlayOneShot(ManHurt);
    }

    public static void playComputerWindowsClip()
    {
        audioSrc.PlayOneShot(Computer_Windows);
    }

    public static void playMagicClip()
    {
        audioSrc.PlayOneShot(magic);
    }

    public static void playGirlClip()
    {
        audioSrc.PlayOneShot(girl);
    }

    public static void playBossDefeatClip()
    {
        audioSrc.PlayOneShot(bossDefeat);
    }
    public static void playHitHWMonsClip()
    {
        audioSrc.PlayOneShot(hitHWMons);
    }
    
    public static void playHWMonsComingClip()
    {
        audioSrc.PlayOneShot(hwmonsComing);
    }
    public static void playPuzzleSolvedClip()
    {
        audioSrc.PlayOneShot(puzzleSolved);
    }
    public static void playTeacherMonsComingClip()
    {
        audioSrc.PlayOneShot(teacherMonscoming);
    }
    public static void playDizzyClip()
    {
        audioSrc.PlayOneShot(dizzy);
    }
    public static void playPeerClip()
    {
        audioSrc.PlayOneShot(peer);
    }
    public static void playScaredBreathClip()
    {
        audioSrc.PlayOneShot(scaredBreath);
    }
    public static void playQustionClip()
    {
        audioSrc.PlayOneShot(question);
    }
    public static void playTiredClip()
    {
        audioSrc.PlayOneShot(tired);
    }
}
