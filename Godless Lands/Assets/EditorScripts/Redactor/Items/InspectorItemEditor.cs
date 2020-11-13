#if UNITY_EDITOR
using Items;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;


namespace ItemsRedactor
{
    [CustomEditor(typeof(ItemEditor))]
    [CanEditMultipleObjects]
    public class InspectorItemEditor : Editor
    {
        private Vector2 scroll;

        public override void OnInspectorGUI()
        {
            //  serializedObject.Update();
            //     EditorGUILayout.HelpBox("id error", MessageType.Error);
            ItemEditor itemEditor = (ItemEditor)target;
            Item _item = itemEditor._item;

            EditorGUILayout.LabelField("ID: " + _item.id);
            _item.texture = EditorGUILayout.ObjectField("Icon", _item.texture, typeof(Texture2D), false) as Texture2D;
            _item.nameItem = EditorGUILayout.TextField("Имя:", _item.nameItem);

            //_text_field.stretchHeight = true;
            //_text_field.alignment 
            //  scroll = EditorGUILayout.BeginScrollView(scroll);
            float _heightLabel = GUI.skin.textField.lineHeight;
            if (_item.description != null) { _heightLabel = GUI.skin.textField.lineHeight * (_item.description.Split('\n').Length + 1); }
            else { _item.description = ""; }
            _item.description = EditorGUILayout.TextField("Описание:", _item.description.Replace(';', '\n'), GUILayout.Height(_heightLabel));
            _item.stack = EditorGUILayout.Toggle("Складировать?", _item.stack);
            _item.weight = EditorGUILayout.IntField("Вес:", _item.weight);
            _item.prefab = EditorGUILayout.ObjectField("Префаб: ", _item.prefab, typeof(GameObject), false) as GameObject;
            //  EditorGUILayout.EndScrollView();
            _item.type = (ItemType)EditorGUILayout.EnumPopup("Используется?", _item.type);


            //Если используемый тип предмета не соответствует созданому классу для сохранения данных.
                if (Item.GetUse(itemEditor.serializableObject) != _item.type)
                {
                switch (_item.type)
                {
                    case ItemType.None:
                        _item.serializableObj = null;
                        return;
                    case ItemType.Weapon:
                        _item.serializableObj = new WeaponItem();
                        break;
                    case ItemType.RestorePoints:
                        _item.serializableObj = new RestorePointsItem();
                        break;
                    case ItemType.Recipes:
                        _item.serializableObj = new RecipesItem();
                        break;
                    default: return;
                }
                itemEditor.serializableObject = _item.serializableObj;
                }


            switch (_item.type)
            {
                case ItemType.Weapon:
                    InspectorItemWeapon.Draw(itemEditor.serializableObject);
                    break;
                case ItemType.Armor:
                    InspectorItemArmor.Draw(itemEditor.serializableObject);
                    break;
                case ItemType.RestorePoints:
                    InspectorItemRestorePoints.Draw(itemEditor.serializableObject);
                    break;
                case ItemType.Recipes:
                    InspectorRecipes.Draw(itemEditor.serializableObject);
                    break;
            }


            if (GUILayout.Button("Save"))
            {
                _item.serializableObj = itemEditor.serializableObject;
            }

            //serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif