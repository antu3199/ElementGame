using UnityEngine;
 
 [ExecuteInEditMode]
 public class SortingLayerOverrider : MonoBehaviour {
 
     [SerializeField]
     private string sortingLayerName = "Default";
 
     [SerializeField]
     private int sortingOrder = 0;
 
     public void OnValidate() {
         apply();
     }
 
     public void OnEnable() {
         apply();
     }
 
     private void apply() {
         var meshRenderer = gameObject.GetComponent<MeshRenderer>();
         meshRenderer.sortingLayerName = sortingLayerName;
         meshRenderer.sortingOrder = sortingOrder;
     }
 }