using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    [SerializeField] private float exitCooldown = 10f;
    [SerializeField] private List<GameObject> minions = new List<GameObject>();
    [SerializeField] private List<Renderer> fadeObjects = new List<Renderer>();

    [SerializeField] private bool combatComplete = false;
    
    [Header("Prefight delay")]
    [SerializeField] private float delayTime = 0;
    [SerializeField] private MonoBehaviour[] delayScripts;


    private void Start() {
        int childcount = transform.childCount;

        for (int i = 0; i < childcount; i++)
        {
            minions.Add(transform.GetChild(i).gameObject);
        }

        foreach (GameObject i in minions)
        {
            i.SetActive(false);
        }
    }

    private void Update() {
        if(transform.childCount <= 0){
            combatComplete = true;
        }

        if (combatComplete)
        {
            UniversalVariables.playerState = PlayerState.Exploring;

            foreach(Renderer r in fadeObjects){
                RevertMaterialToOpaque(r.material);
                Color color = r.material.color;
                color.a = 1f;
                r.material.color = color;
            }

            gameObject.SetActive(false);
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player" && !combatComplete){
            UniversalVariables.playerState = PlayerState.Combat;

            foreach (GameObject i in minions)
            {
                if(i == null){
                    minions.Remove(i);
                }else{
                    i.SetActive(true);
                }
            }

            foreach(Renderer r in fadeObjects){
                SetMaterialToFade(r.material);
                Color color = r.material.color;
                color.a = 0.3f;
                r.material.color = color;
            }

            if(delayTime > 0 && delayScripts.Length > 0){
                StartCoroutine(ScriptDelayer());
            }

            other.gameObject.GetComponent<SpellController>().AddTargets();
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player"){
            StartCoroutine(ExitTimer());
        }
    }

    void SetMaterialToFade(Material material)
    {
        // Check if the shader is compatible (e.g., Standard Shader)
        if (material.shader.name == "Standard")
        {
            // Change the rendering mode to Fade
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000; // Queue for transparent
        }
    }

    public void RevertMaterialToOpaque(Material material)
    {
        // Check if the shader is compatible
        if (material.shader.name == "Standard")
        {
            // Change the rendering mode to Opaque
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = -1; // Reset to default (opaque)
        }
    }

    IEnumerator ExitTimer(){
        yield return new WaitForSeconds(exitCooldown);

        foreach (GameObject i in minions)
        {
            if(i == null){
                    minions.Remove(i);
                }else{
                    i.SetActive(false);
                }
        }

        UniversalVariables.playerState = PlayerState.Exploring;
    }

    IEnumerator ScriptDelayer(){ //AKA the procrastinator
        foreach(MonoBehaviour m in delayScripts){
            m.enabled = false;
        }
        
        yield return new WaitForSeconds(delayTime);

        foreach(MonoBehaviour m in delayScripts){
            m.enabled = true;
        }
    }

}
