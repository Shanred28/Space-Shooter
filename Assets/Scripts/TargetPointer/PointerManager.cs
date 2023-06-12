using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PointerManager : SingletonBase<PointerManager>
    {
        [SerializeField] private PointerIcon m_PointerIconPrefab;
        [SerializeField] private SuppliesIcon m_SuppliesIconPrefab;
        [SerializeField] private TargetMissionIcon m_TargetMissionIconPrefab;

        private Dictionary<TargetMissionPointer, TargetMissionIcon> dictionaryTargetMission = new Dictionary<TargetMissionPointer, TargetMissionIcon>();
        private Dictionary<EnemyPointer, PointerIcon> dictionaryEnemy = new Dictionary<EnemyPointer, PointerIcon>();
        private Dictionary<SuppliesPointer, SuppliesIcon> dictionarySupplies = new Dictionary<SuppliesPointer, SuppliesIcon>();

        [SerializeField] private Player player;
        [SerializeField] private new Camera camera;

        private bool IsLevelFinish = false;

        private void Start()
        {
            LevelSequenceController.Instance.IsLevelFinish.AddListener(GameProcess);
        }

        public void AddToListEnemy(EnemyPointer enemyPointer)
        {
            PointerIcon newPointer = Instantiate(m_PointerIconPrefab, transform);           
            dictionaryEnemy.Add(enemyPointer, newPointer);
        }

        public void AddToListSupplies(SuppliesPointer suppliesPointer)
        {
            SuppliesIcon newSuppliesPinter = Instantiate(m_SuppliesIconPrefab, transform);
            dictionarySupplies.Add(suppliesPointer, newSuppliesPinter);
        }

        public void RemoveToListEnemy(EnemyPointer enemyPointer) 
        {
            Destroy(dictionaryEnemy[enemyPointer].gameObject);
            dictionaryEnemy.Remove(enemyPointer);
        }

        public void RemoveToListSupplies(SuppliesPointer suppliesPointer)
        {
            Destroy(dictionarySupplies[suppliesPointer].gameObject);
            dictionarySupplies.Remove(suppliesPointer);
        }


        public void AddToListTargetMission(TargetMissionPointer targetMissionPointer)
        {
            TargetMissionIcon newTargetMissionPointer = Instantiate(m_TargetMissionIconPrefab, transform);
            dictionaryTargetMission.Add(targetMissionPointer, newTargetMissionPointer);
        }

        public void RemoveToListTargetMission(TargetMissionPointer targetMissionPointer)
        {
            Destroy(dictionaryTargetMission[targetMissionPointer].gameObject);
            dictionaryTargetMission.Remove(targetMissionPointer);
        }

        private void LateUpdate()
        {
            if (IsLevelFinish == false)
            {
                Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
               

                foreach (var kvp in dictionaryEnemy)
                {
                    EnemyPointer enemyPointer = kvp.Key;
                    PointerIcon pointerIcon = kvp.Value;
                    Vector3 toEnemy = enemyPointer.transform.position - player.ActiveShip.transform.position;
                    Ray ray = new Ray(player.ActiveShip.transform.position, toEnemy);


                    float rayMinDistance = Mathf.Infinity;
                    int index = 0;

                    for (int i = 0; i < 4; i++)
                    {
                        if (planes[i].Raycast(ray, out float distance))
                        {
                            if (distance < rayMinDistance)
                            {
                                rayMinDistance = distance;
                                index = i;
                            }
                        }
                    }

                    rayMinDistance = Mathf.Clamp(rayMinDistance, 0, toEnemy.magnitude);
                    Vector3 worldPosition = ray.GetPoint(rayMinDistance);
                    Vector3 position = camera.WorldToScreenPoint(worldPosition);
                    Quaternion rotation = GetIconRotation(index);

                    if (toEnemy.magnitude > rayMinDistance)
                    {
                        pointerIcon.Show();
                    }

                    else
                        pointerIcon.Hide();

                    pointerIcon.SetIconPosition(position, rotation);
                }

                foreach (var kvp in dictionarySupplies)
                {
                    SuppliesPointer suppliesPointer = kvp.Key;
                    SuppliesIcon suppliesIcon = kvp.Value;
                    Vector3 toSupplies = suppliesPointer.transform.position - player.ActiveShip.transform.position;

                    Ray ray = new Ray(player.ActiveShip.transform.position, toSupplies);

                    float rayMinDistance = Mathf.Infinity;
                    int index = 0;

                    for (int i = 0; i < 4; i++)
                    {
                        if (planes[i].Raycast(ray, out float distance))
                        {
                            if (distance < rayMinDistance)
                            {
                                rayMinDistance = distance;
                                index = i;
                            }
                        }
                    }
                    rayMinDistance = Mathf.Clamp(rayMinDistance, 0, toSupplies.magnitude);
                    Vector3 worldPosition = ray.GetPoint(rayMinDistance);
                    Vector3 position = camera.WorldToScreenPoint(worldPosition);
                    Quaternion rotation = GetIconRotation(index);

                    if (toSupplies.magnitude > rayMinDistance)
                    {
                        suppliesIcon.Show();
                    }

                    else
                        suppliesIcon.Hide();

                    suppliesIcon.SetIconPosition(position, rotation);
                }

                foreach (var kvp in dictionaryTargetMission)
                {
                    TargetMissionPointer targetPointer = kvp.Key;
                    TargetMissionIcon targetIcon = kvp.Value;
                    Vector3 toSupplies = targetPointer.transform.position - player.ActiveShip.transform.position;

                    Ray ray = new Ray(player.ActiveShip.transform.position, toSupplies);

                    float rayMinDistance = Mathf.Infinity;
                    int index = 0;

                    for (int i = 0; i < 4; i++)
                    {
                        if (planes[i].Raycast(ray, out float distance))
                        {
                            if (distance < rayMinDistance)
                            {
                                rayMinDistance = distance;
                                index = i;
                            }
                        }
                    }
                    rayMinDistance = Mathf.Clamp(rayMinDistance, 0, toSupplies.magnitude);
                    Vector3 worldPosition = ray.GetPoint(rayMinDistance);
                    Vector3 position = camera.WorldToScreenPoint(worldPosition);
                    Quaternion rotation = GetIconRotation(index);

                    if (toSupplies.magnitude > rayMinDistance)
                    {
                        targetIcon.Show();
                    }

                    else
                        targetIcon.Hide();

                    targetIcon.SetIconPosition(position, rotation);
                }
            }                                  
        }     

        Quaternion GetIconRotation(int planeIndex)
        {
            if (planeIndex == 0)
            {
                return Quaternion.Euler(0f, 0f, 90f);
            }
            else if (planeIndex == 1)
            {
                return Quaternion.Euler(0f, 0f, -90f);
            }
            else if (planeIndex == 2)
            {
                return Quaternion.Euler(0f, 0f, 180f);
            }
            else if (planeIndex == 3)
            {
                return Quaternion.Euler(0f, 0f, 0f);
            }
            return Quaternion.identity;
        }

        private void GameProcess()
        {
            IsLevelFinish = true;
            LevelSequenceController.Instance.IsLevelFinish.RemoveListener(GameProcess);
        }
    }
}
