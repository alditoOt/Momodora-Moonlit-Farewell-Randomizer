using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MomoReader
{
    public class MomoReader : MelonMod
    {
        public static string sceneName;
        public static float phys_attack;
        Vector2 playerPos = new Vector2();
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            MomoReader.sceneName = sceneName;
            if (sceneName == "Bark08")
            {
                GameObject targetPlatform = GameObject.FindObjectsOfType<GameObject>()
     .FirstOrDefault(obj => obj.name == "CollisionBlockGhost_Bark");

                GameObject platform = GameObject.Instantiate(targetPlatform); // creates a platform
                platform.transform.position = new Vector3(5240, -1816.914f, -98);
                //platform.transform.localScale = new Vector3(3, 0.5f, 1); // make it look like a platform

                // Optional: Add components like Rigidbody, Collider, or a custom script
            }
            //5240 - 1880 - 98;
        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnInitializeMelon()
        {
            MelonEvents.OnGUI.Subscribe(DrawMenu, 100); // The higher the value, the lower the priority.
        }

        private void DrawMenu()
        {
            GUI.Box(new Rect(0, 0, 300, 500), "My Menu");
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.F5)) {
                MelonLogger.Msg("Key down");
                //Time.timeScale = 50f;
                Collider2D col = Physics2D.OverlapCircle(playerPos, 1f);

                //foreach (Collider2D col in colliders)
                //{
                    MelonLogger.Msg($"Found collider: {col.gameObject.name}, Layer: {col.gameObject.layer}, Tag: {col.gameObject.tag}");
                //}
            }
            if (Input.GetKeyUp(KeyCode.F6))
            {
                Vector3 playerPos = GameObject.Find("PlayerPrefab").transform.position;
                Collider[] colliders = Physics.OverlapSphere(playerPos, 10f);

                foreach (Collider col in colliders)
                {
                    MelonLogger.Msg($"Found collider: {col.gameObject.name}, Layer: {col.gameObject.layer}, Tag: {col.gameObject.tag}");
                }

            }
            if (Input.GetKeyUp(KeyCode.F5))
            {
                //Time.timeScale = 1f;
            } 
        }

    }
}
