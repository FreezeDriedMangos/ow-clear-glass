using OWML.Common;
using OWML.ModHelper;
using System.Linq;
using UnityEngine;

namespace ClearGlass
{
    public class ClearGlass : ModBehaviour
    {
        private void Start()
        {
            // Example of accessing game code.
            LoadManager.OnCompleteSceneLoad += (scene, loadScene) =>
            {
                if (loadScene != OWScene.SolarSystem) return;     
                
                ModHelper.Events.Unity.FireInNUpdates(() =>
                {
                    // get some nice glass materials
                    var clearGlassMat = GameObject.Find("Ship_Body/Module_Cockpit/Geo_Cockpit/Cockpit_Geometry/Cockpit_Interior/Cockpit_TransparentGlass_Structural").GetComponent<MeshRenderer>().material;
                    var nomaiianClearGlassMat = GameObject.Find("CaveTwin_Body/Sector_CaveTwin/Interactables_CaveTwin/Structure_NOM_EyeSymbol/Structure_NOM_EyeSymbol_Glass").GetComponent<MeshRenderer>().materials[1];
                    
                    // clear up all other glass
                    foreach(var renderer in Resources.FindObjectsOfTypeAll<MeshRenderer>())
                    {
                        var materials = renderer.sharedMaterials;
                        for (var i = 0; i < materials.Length; i++)
                        {
                            if (!materials[i]) continue;
                            if (materials[i].name.StartsWith("ShipExterior_HEA_VillageCloth_mat")) materials[i] = clearGlassMat;
                            else if (materials[i].name.StartsWith("Structure_NOM_Glass_Opaque_mat")) materials[i] = nomaiianClearGlassMat;
                        }
                        renderer.sharedMaterials = materials;
                    }

                    // done!
                    ModHelper.Console.WriteLine($"{nameof(ClearGlass)} finished loading!", MessageType.Success);

                }, 100);
            };
        }
    }
}
