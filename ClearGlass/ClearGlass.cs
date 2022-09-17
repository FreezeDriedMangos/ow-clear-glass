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
                        var materials = new Material[renderer.materials.Length]; 
                        bool needsSwap = false;
                        for (var i = 0; i < renderer.materials.Length; i++)
                        {
                            materials[i] = renderer.materials[i];   
                            if      (renderer.materials[i].name == "ShipExterior_HEA_VillageCloth_mat (Instance)") { materials[i] = clearGlassMat;         needsSwap = true; }
                            else if (renderer.materials[i].name == "Structure_NOM_Glass_Opaque_mat (Instance)")    { materials[i] = nomaiianClearGlassMat; needsSwap = true; }
                        }

                        if (needsSwap) renderer.materials = materials;
                    }

                    // done!
                    ModHelper.Console.WriteLine($"{nameof(ClearGlass)} finished loading!", MessageType.Success);

                }, 100);
            };
        }
    }
}
