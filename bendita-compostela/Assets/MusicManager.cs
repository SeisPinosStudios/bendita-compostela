using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class MusicManager : PersistentSingleton <MusicManager>
{
    FMOD.Studio.EventInstance gameMusic;

    /*
    Fases de cinematica:
    gameMusic.setParameterByName("cinematica", 0);
    gameMusic.setParameterByName("cinematica", 1);
    gameMusic.setParameterByName("cinematica", 2);
    gameMusic.setParameterByName("cinematica", 3);
    gameMusic.setParameterByName("cinematica", 4);
    */

   void Start()
   {

   }

   public void PlayMusic(string nombre)
   {
    gameMusic = FMODUnity.RuntimeManager.CreateInstance(nombre); //"event:/bendita Compostela"
    gameMusic.start();
   }

   public void CambiarParametro(string nombre, int valor)
   {
    gameMusic.setParameterByName(nombre, valor);
   }

   public void Parar()
   {
   	gameMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
   }


}