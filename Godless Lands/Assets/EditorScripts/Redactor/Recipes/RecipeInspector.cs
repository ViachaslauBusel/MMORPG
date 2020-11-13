#if UNITY_EDITOR
using Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Recipes
{
    [CustomEditor(typeof(RecipeEditor))]
    [CanEditMultipleObjects]
    public class RecipeInspector : Editor
    {
        private static ItemsContainer itemsList;


        public override void OnInspectorGUI()
        {
         //   EditorGUIUtility.labelWidth = 10.0f;
            Recipe recipe = ((RecipeEditor)target).GetSelectObject() as Recipe;

            GUILayout.Space(15.0f);

            GUILayout.Label("ID: " + recipe.id);

            GUILayout.Space(10.0f);

           // itemsList = Project.ItemsList;

            recipe.use = (MachineUse) EditorGUILayout.EnumPopup(recipe.use);
            recipe.result = EditorGUILayout.IntField("result:", recipe.result);
            DrawItem(recipe.result);

            recipe.profession = (ProfessionEnum)EditorGUILayout.EnumPopup(recipe.profession);
            recipe.exp = EditorGUILayout.IntField("Exp:", recipe.exp);
            recipe.stamina = EditorGUILayout.IntField("Stamina:", recipe.stamina);

            if (GUILayout.Button("Add component"))
            {
                recipe.component.Add(new Piece());
            }

            DrawComponent(recipe.component);

            if (GUILayout.Button("Add fuel"))
            {
                recipe.fuel.Add(new Piece());
            }

           DrawComponent(recipe.fuel);

        }

        public static void DrawComponent(List<Piece> pieces)
        {
            Piece remove = null;
            for (int i = 0; i < pieces.Count; i++)
            {
                GUILayout.BeginVertical(EditorStyles.helpBox);
                GUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("ID:", GUILayout.Width(30.0f));
                pieces[i].ID = EditorGUILayout.IntField(pieces[i].ID);

                EditorGUILayout.LabelField("count:", GUILayout.Width(40.0f));
                pieces[i].count = EditorGUILayout.IntField(pieces[i].count);
                GUILayout.EndHorizontal();
                if (DrawItemComponent(pieces[i].ID)) { remove = pieces[i]; }
                GUILayout.EndVertical();
            }
            if(remove != null)
            {
                pieces.Remove(remove);
            }
        }

        public static bool DrawItemComponent(int id)
        {
            if (itemsList == null)
            {
                itemsList = Project.ItemsList;
              //  return false;
            }
            Item item = itemsList.GetItem(id);
            GUILayout.BeginHorizontal();
            if (item != null)
            {
                GUILayout.Label(item.texture, GUILayout.Width(20), GUILayout.Height(20));
                GUILayout.Label(item.nameItem);
            }
            else
            {
                GUILayout.FlexibleSpace();
            }
            if (GUILayout.Button("remove", GUILayout.Width(60))) return true;
            GUILayout.EndHorizontal();
            return false;
        }

        private void DrawItem(int id)
        {
            if (itemsList == null) return;
            Item item = itemsList.GetItem(id);
            if (item != null)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(item.texture, GUILayout.Width(20), GUILayout.Height(20));
                GUILayout.Label(item.nameItem);
                GUILayout.EndHorizontal();
            }
        }
    }
}
#endif