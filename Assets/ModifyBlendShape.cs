using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ModifyBlendShape : MonoBehaviour
{
    public GameObject avatarMesh;
    Slider[] sliders;

    public TextAsset shapeFile;

    SkinnedMeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = avatarMesh.GetComponent<SkinnedMeshRenderer>();
        sliders = GameObject.Find("Sliders").GetComponentsInChildren<Slider>();

        for(int i = 0; i < sliders.Length; i++) {
            print("blender");
            int index = sliders[i].gameObject.GetComponent<BlendshapeSlider>().index;
            Slider current = sliders[i];
            current.onValueChanged.AddListener(delegate {UpdateBlendShape(current.value, index);});
        }

    }

    void UpdateBlendShape(float newValue, int index) {

        ShapeParms newShape = JsonUtility.FromJson<ShapeParms>(shapeFile.text);
        newShape.betas[index] = newValue;

        shapeFile = new TextAsset(JsonUtility.ToJson(newShape));

        SMPLBlendshapes smpl = avatarMesh.GetComponent<SMPLBlendshapes>();
        smpl.shapeParmsJSON = shapeFile;
        smpl.readShapeParms();
        smpl.setShapeBlendValues();

        PlayerPrefs.SetString("shapeFile", shapeFile.text);

    }

    public void RotateModel(bool direction) {
        //true is left, false is right
        if(direction) {
            GameObject.Find("f_model").transform.Rotate(new Vector3(0, 15, 0));
        }
        else {
            GameObject.Find("f_model").transform.Rotate(new Vector3(0, -15, 0));
        }
    }

    public void ResetModel() {
        print("reset");
        for(int i = 0; i < 9; i++) {
            UpdateBlendShape(0f, i);
            sliders[i].value = 0;
        }

    }

}

class ShapeParms {
    public float[] betas;
}
