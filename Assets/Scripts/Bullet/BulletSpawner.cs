#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
public class BulletSpawner : BulletSpawnerBase
{
#if UNITY_EDITOR
    [CanEditMultipleObjects]
    [CustomEditor(typeof(BulletSpawner))]
    public class Inspector : Editor
    {
        SerializedProperty directionSettings;
        SerializedProperty inverseDirectionSettings;
        SerializedProperty targetEnemySettings;

        SerializedProperty overrideCollisionSettings;

        SerializedProperty audioProperties;

        SerializedProperty test;
        SerializedProperty timer;
        void OnEnable()
        {
            directionSettings = serializedObject.FindProperty("directionSettings");
            inverseDirectionSettings = serializedObject.FindProperty("inverseDirectionSettings");
            targetEnemySettings = serializedObject.FindProperty("targetEnemySettings");

            audioProperties = serializedObject.FindProperty("audioProperties");

            overrideCollisionSettings = serializedObject.FindProperty("overrideCollisionSettings");
        }
        override public void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            serializedObject.Update();

            timer = serializedObject.FindProperty("timer");

            var myScript = target as BulletSpawner;

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour(myScript), typeof(BulletSpawner), false);
            EditorGUI.EndDisabledGroup();

            myScript.automatically = EditorGUILayout.Toggle("Automatically", myScript.automatically);
            myScript.test = EditorGUILayout.FloatField("Test", myScript.test);

            EditorGUILayout.PropertyField(timer, new GUIContent("Timer"));

            myScript.id = EditorGUILayout.TextField("Id", myScript.id);

            myScript.prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", myScript.prefab, typeof(GameObject), true);
            myScript.parent = (Transform)EditorGUILayout.ObjectField("Parent", myScript.parent, typeof(Transform), true);

            myScript.position = EditorGUILayout.Vector3Field("Position", myScript.position);
            myScript.scale = EditorGUILayout.FloatField("Scale", myScript.scale);

            myScript.speed = EditorGUILayout.FloatField("Speed", myScript.speed);
            myScript.speedRange = EditorGUILayout.FloatField("Speed Range", myScript.speedRange);

            if (myScript.speed < 1) myScript.speed = 1;
            if (myScript.speedRange < 0) myScript.speedRange = 0;

            EditorGUILayout.PropertyField(audioProperties, new GUIContent("Audio Properties"));

            myScript.mode = (Settings.Mode)EditorGUILayout.EnumPopup("Mode", myScript.mode);

            if (myScript.mode == Settings.Mode.Direction)
            {
                EditorGUILayout.PropertyField(directionSettings, new GUIContent("Direction Settings"));
            }
            else if (myScript.mode == Settings.Mode.InverseDirection)
            {
                EditorGUILayout.PropertyField(inverseDirectionSettings, new GUIContent("Inverse Direction Settings"));
            }
            else if (myScript.mode == Settings.Mode.TargetEnemy)
            {
                EditorGUILayout.PropertyField(targetEnemySettings, new GUIContent("Target Enemy Settings"));
            }

            EditorGUILayout.PropertyField(overrideCollisionSettings, new GUIContent("Override Collision Settings"));

            myScript.prefabSmartOverrides = EditorGUILayout.Toggle("Prefab Smart Overrides", myScript.prefabSmartOverrides);

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(myScript);

                serializedObject.ApplyModifiedProperties();
            }
        }
    }
#endif

    [System.Serializable]
    public class CollisionSettings
    {
        public bool useTheseSettings;
        public int structuralIntegrity = 1;
    }

    [System.Serializable]
    public class DirectionSettings
    {
        [System.Serializable]
        public class TriggerSettings
        {
#if UNITY_EDITOR
            [CustomPropertyDrawer(typeof(TriggerSettings))]
            public class Drawer : PropertyDrawer
            {
                public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
                {
                    Rect rect;

                    float y = 0;

                    EditorGUI.BeginProperty(position, label, property);

                    var indent = EditorGUI.indentLevel;

                    rect = new Rect(position.min.x, position.min.y + y, position.size.x, EditorGUIUtility.singleLineHeight); y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    EditorGUI.PropertyField(rect, property.FindPropertyRelative("mode"));

                    if (property.FindPropertyRelative("mode").enumValueIndex == 1)
                    {
                        EditorGUI.indentLevel = 1;

                        rect = new Rect(position.min.x, position.min.y + y, position.size.x, EditorGUIUtility.singleLineHeight); y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                        EditorGUI.PropertyField(rect, property.FindPropertyRelative("Button1"));
                        rect = new Rect(position.min.x, position.min.y + y, position.size.x, EditorGUIUtility.singleLineHeight); y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                        EditorGUI.PropertyField(rect, property.FindPropertyRelative("Button2"));
                        rect = new Rect(position.min.x, position.min.y + y, position.size.x, EditorGUIUtility.singleLineHeight); y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                        EditorGUI.PropertyField(rect, property.FindPropertyRelative("autofire"));

                        if (property.FindPropertyRelative("autofire").boolValue == true)
                        {
                            EditorGUI.indentLevel += 1;

                            rect = new Rect(position.min.x, position.min.y + y, position.size.x, EditorGUIUtility.singleLineHeight); y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                            EditorGUI.PropertyField(rect, property.FindPropertyRelative("rapidFire"));
                        }
                    }

                    EditorGUI.indentLevel = indent;

                    EditorGUI.EndProperty();
                }

                public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
                {
                    float totalLines = 1;

                    if (property.FindPropertyRelative("mode").enumValueIndex == 1)
                    {
                        totalLines += 3;

                        if (property.FindPropertyRelative("autofire").boolValue == true)
                        {
                            totalLines += 1;
                        }
                    }

                    return (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * totalLines;
                }
            }
#endif
            public enum Mode { Auto, Player, Smart };

            public Mode mode;
            public KeyCode Button1 = KeyCode.Joystick1Button0;
            public KeyCode Button2 = KeyCode.None;
            public bool autofire;
            public bool rapidFire;
        }

        public Vector3 rotation;
        [Header("Trigger Settings")]
        public TriggerSettings triggerSettings;
    }

    [System.Serializable]
    public class InverseDirectionSettings
    {
        public Vector3 position;
    }

    [System.Serializable]
    public class TargetEnemySettings
    {
        public bool toughestFirst;
        public float targetRange;
    }

    [System.Serializable]
    public class Settings
    {
        //public enum Mode { TargetPlayer, Random, Direction };
        public bool useTheseSettings;
        public GameObject prefab;
        public float scale = 1;
        public float interval = 2;
        public float intervalRange = 6;
        public float speed = 4;
        public float speedRange;
        public int counter = -1;

        public AudioProperties audioProperties;
        public CollisionSettings overrideCollisionSettings;
        public enum Mode
        {
            TargetPlayer,   // 플레이어를 타겟으로 하는 탄막
            TargetEnemy, // 적을 타겟으로 하는 탄막
            Random,     // 랜덤 방향으로 이동하는 탄막
            Direction,
            OneDirection,   // 한가지 방향으로 이동하는 탄막
            InverseDirection, // 타겟과 역방향으로 이동하는 탄막 (플레이어 탄)
            Sector,     // 부채꼴 탄막
            Circular,    // 원형 탄막

            // TODO
            Dinamic     // 설치탄
        };
        public Mode mode;
    }

    public float test = 0;

    public GameObject prefab;
    public Transform parent;
    public bool prefabSmartOverrides = true;
    public Settings.Mode mode = Settings.Mode.TargetPlayer;
    public Vector3 position;
    public float scale = 1;
    public float speed = 8;
    [Tooltip("Speed Range. Any value over zero triggers random  mode (random value is between speed and speed + speedRange)")]
    public float speedRange;
    public TargetEnemySettings targetEnemySettings = new TargetEnemySettings();
    public DirectionSettings directionSettings = new DirectionSettings();
    public InverseDirectionSettings pointInSpaceSettings = new InverseDirectionSettings();
    public CollisionSettings overrideCollisionSettings = new CollisionSettings();
    int checkCount = 0;

    public AudioProperties audioProperties;

    public void Set(Settings settings)
    {
        prefab = settings.prefab;

        scale = settings.scale;

        timer.timer = Random.Range(settings.interval, settings.interval + settings.intervalRange);

        timer.interval = settings.interval;
        timer.intervalRange = settings.intervalRange;
        timer.counter = settings.counter;
        speed = settings.speed;
        speedRange = settings.speedRange;
        audioProperties = settings.audioProperties;
        overrideCollisionSettings = settings.overrideCollisionSettings;

        prefabSmartOverrides = true;
    }

    public override void UpdateSpawner()
    {
        if (prefab == null) return;

        if (mode == Settings.Mode.Direction)
        {
            if (directionSettings.triggerSettings.mode == DirectionSettings.TriggerSettings.Mode.Auto)
            {
                if (timer.Update()) OnSpawn();
            }
            else if (directionSettings.triggerSettings.mode == DirectionSettings.TriggerSettings.Mode.Smart && BulletSpawnerHelper.count > 0)
            {
                if (timer.Update()) OnSpawn();
            }
            else if (directionSettings.triggerSettings.mode == DirectionSettings.TriggerSettings.Mode.Player)
            {
                if (directionSettings.triggerSettings.autofire)
                {
                    if (Input.GetKey(directionSettings.triggerSettings.Button1) || Input.GetKey(directionSettings.triggerSettings.Button2))
                    {
                        if (timer.Update()) OnSpawn();
                    }
                    else if (directionSettings.triggerSettings.rapidFire == true)
                    {
                        timer.timer = 0;
                    }
                }
                else
                {
                    if (Input.GetKeyDown(directionSettings.triggerSettings.Button1) || Input.GetKeyDown(directionSettings.triggerSettings.Button2))
                    {
                        if (timer.Countdown()) OnSpawn();
                    }
                }
            }
        }
        else
        {
            if (timer.Update()) OnSpawn();
        }
    }
    public override void IgnitionInit()
    {
        if (prefab && prefab.scene.rootCount > 0) prefab.SetActive(false);
    }
    public override void OnSpawn()
    {
        if (mode == Settings.Mode.TargetPlayer)     // 플레이어를 타겟으로
        {
            var target = PlayersGroup.GetRandom();
            if (target)
            {
                GameObject clone = null;
                var b = prefab.GetComponent<BulletBase>();
                if (b != null)
                {
                    var bullet = BulletPool.Instance.RentBullet(b.type, transform.position, prefab.transform.rotation);
                    if (bullet != null)
                        clone = bullet.gameObject;
                }
                if (!clone)
                    clone = Instantiate(prefab, transform.position, prefab.transform.rotation);

                if (clone)
                {
                    clone.transform.localScale *= scale;
                    clone.transform.Translate(position, Space.Self);

                    if (parent) clone.transform.parent = parent;

                    if (overrideCollisionSettings.useTheseSettings) _OverrideCollisionSettings(clone);

                    if (prefabSmartOverrides) _SmartOverrides(clone);

                    var bulletBase = clone.GetComponent<BulletBase>();
                    if (bulletBase)
                    {
                        var angle = Mathf.Atan2(target.transform.position.y - clone.transform.position.y, target.transform.position.x - clone.transform.position.x);

                        clone.transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);

                        bulletBase.velocity = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * Random.Range(speed, speed + speedRange);
                    }

                    clone.SetActive(true);
                }
            }
        }
        else if (mode == Settings.Mode.TargetEnemy)
        {
            var target = Targetable.GetClosest(gameObject, targetEnemySettings.toughestFirst, targetEnemySettings.targetRange);
            if (target != null)
            {
                GameObject clone = null;
                var b = prefab.GetComponent<BulletBase>();
                if (b != null)
                {
                    var bullet = BulletPool.Instance.RentBullet(b.type, transform.position, transform.rotation);
                    if (bullet != null)
                        clone = bullet.gameObject;
                }
                if (!clone)
                    clone = Instantiate(prefab, transform.position, transform.rotation);

                if (clone)
                {
                    clone.transform.localScale *= scale;
                    clone.transform.Translate(position, Space.Self);

                    if (parent) clone.transform.parent = parent;

                    if (overrideCollisionSettings.useTheseSettings) _OverrideCollisionSettings(clone);

                    if (prefabSmartOverrides) _SmartOverrides(clone);

                    clone.SetActive(true);

                    var bulletBase = clone.GetComponent<BulletBase>();
                    if (bulletBase)
                    {
                        var angle = Mathf.Atan2(target.gameObject.transform.position.y - clone.transform.position.y, target.gameObject.transform.position.x - clone.transform.position.x);

                        clone.transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);

                        bulletBase.velocity = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * Random.Range(speed, speed + speedRange);
                    }
                    else
                    {
                        var rb = clone.GetComponent<Rigidbody2D>();
                        if (rb)
                        {
                            var angle = Mathf.Atan2(target.gameObject.transform.position.y - clone.transform.position.y, target.gameObject.transform.position.x - clone.transform.position.x);

                            clone.transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);

                            rb.velocity = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * Random.Range(speed, speed + speedRange);
                        }
                    }
                }
            }
        }
        else if (mode == Settings.Mode.Random)  // 랜덤
        {
            GameObject clone = null;
            var b = prefab.GetComponent<BulletBase>();
            if (b != null)
            {
                var bullet = BulletPool.Instance.RentBullet(b.type, transform.position, transform.rotation);
                if (bullet != null)
                    clone = bullet.gameObject;
            }
            if (!clone)
                clone = Instantiate(prefab, transform.position, transform.rotation);

            if (clone)
            {
                clone.transform.localScale *= scale;
                clone.transform.Translate(position, Space.Self);

                if (parent) clone.transform.parent = parent;

                if (overrideCollisionSettings.useTheseSettings) _OverrideCollisionSettings(clone);

                if (prefabSmartOverrides) _SmartOverrides(clone);

                clone.SetActive(true);

                var bulletBase = clone.GetComponent<BulletBase>();
                if (bulletBase)
                {
                    var angle = Random.Range(0, 359) * Mathf.Deg2Rad;

                    clone.transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);

                    bulletBase.velocity = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * Random.Range(speed, speed + speedRange);
                }
            }
        }
        else if (mode == Settings.Mode.Direction)
        {
            GameObject clone = null;
            var b = prefab.GetComponent<BulletBase>();
            if (b != null)
            {
                var bullet = BulletPool.Instance.RentBullet(b.type, transform.position, transform.rotation * Quaternion.Euler(directionSettings.rotation));
                if (bullet != null)
                    clone = bullet.gameObject;
            }
            if (!clone)
                clone = Instantiate(prefab, transform.position, transform.rotation * Quaternion.Euler(directionSettings.rotation));

            if (clone)
            {
                clone.transform.localScale *= scale;

                clone.transform.Translate(position, Space.Self);

                if (parent) clone.transform.parent = parent;

                if (overrideCollisionSettings.useTheseSettings) _OverrideCollisionSettings(clone);

                if (prefabSmartOverrides) _SmartOverrides(clone);

                clone.SetActive(true);

                var bulletBase = clone.GetComponent<BulletBase>();
                if (bulletBase)
                {
                    bulletBase.velocity = clone.transform.rotation * new Vector3(Random.Range(speed, speed + speedRange), 0, 0);
                }
                else
                {
                    var rb = clone.GetComponent<Rigidbody2D>();
                    if (rb) rb.velocity = clone.transform.rotation * new Vector3(Random.Range(speed, speed + speedRange), 0, 0);
                    //Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), clone.GetComponent<Collider2D>());
                }
            }
        }
        else if (mode == Settings.Mode.InverseDirection)
        {
            GameObject clone = null;
            var b = prefab.GetComponent<BulletBase>();
            if (b != null)
            {
                var bullet = BulletPool.Instance.RentBullet(b.type, transform.position, transform.rotation);
                if (bullet != null)
                    clone = bullet.gameObject;
            }
            if (!clone)
                clone = Instantiate(prefab, transform.position, transform.rotation);

            if (clone)
            {
                clone.transform.localScale *= scale;
                clone.transform.Translate(position, Space.Self);

                if (parent) clone.transform.parent = parent;

                if (overrideCollisionSettings.useTheseSettings) _OverrideCollisionSettings(clone);

                if (prefabSmartOverrides) _SmartOverrides(clone);

                clone.SetActive(true);

                var bulletBase = clone.GetComponent<BulletBase>();
                if (bulletBase)
                {
                    var angle = Mathf.Atan2(pointInSpaceSettings.position.y - clone.transform.position.y, pointInSpaceSettings.position.x - clone.transform.position.x);

                    clone.transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);

                    bulletBase.velocity = MathHelpers.GetVelocity(clone.transform.position, pointInSpaceSettings.position) * Random.Range(speed, speed + speedRange);
                }
            }
        }
        else if (mode == Settings.Mode.OneDirection)    // 직선
        {
            Vector2 target = transform.position;
            target.y = -5.5f;

            //var target = PlayersGroup.GetRandom();
            //if (target)
            //{

            GameObject clone = null;
            var b = prefab.GetComponent<BulletBase>();
            if (b != null)
            {
                var bullet = BulletPool.Instance.RentBullet(b.type, transform.position, prefab.transform.rotation);
                if (bullet != null)
                    clone = bullet.gameObject;
            }
            if (!clone)
                clone = Instantiate(prefab, transform.position, prefab.transform.rotation);

            if (clone)
            {
                clone.transform.localScale *= scale;
                clone.transform.Translate(position, Space.Self);

                if (parent) clone.transform.parent = parent;

                if (overrideCollisionSettings.useTheseSettings) _OverrideCollisionSettings(clone);

                if (prefabSmartOverrides) _SmartOverrides(clone);

                var bulletBase = clone.GetComponent<BulletBase>();
                if (bulletBase)
                {
                    var angle = Mathf.Atan2(target.y - clone.transform.position.y, target.x - clone.transform.position.x);

                    clone.transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);

                    bulletBase.velocity = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * Random.Range(speed, speed + speedRange);
                }

                clone.SetActive(true);
            }
        }
        else if (mode == Settings.Mode.Sector)     // 부채꼴
        {
            GameObject clone = null;
            var b = prefab.GetComponent<BulletBase>();
            if (b != null)
            {
                var bullet = BulletPool.Instance.RentBullet(b.type, transform.position, transform.rotation);
                if (bullet != null)
                    clone = bullet.gameObject;
            }
            if (!clone)
                clone = Instantiate(prefab, transform.position, transform.rotation);

            if (clone)
            {
                clone.transform.localScale *= scale;
                clone.transform.Translate(position, Space.Self);

                if (parent) clone.transform.parent = parent;

                if (overrideCollisionSettings.useTheseSettings) _OverrideCollisionSettings(clone);

                if (prefabSmartOverrides) _SmartOverrides(clone);

                clone.SetActive(true);

                var bulletBase = clone.GetComponent<BulletBase>();
                if (bulletBase)
                {
                    //var angle = Mathf.Sin(Mathf.PI * Time.time * test) * 180 / Mathf.PI;
                    var angle = (Mathf.Cos(Mathf.PI * Time.time * 20) * 90 / Mathf.PI) - 90;
                    clone.transform.eulerAngles = new Vector3(0, 0, angle);
                    bulletBase.velocity = Quaternion.Euler(0, 0, angle) * Vector3.right * speed;
                }
            }


        }
        else if (mode == Settings.Mode.Circular)   // 원형 탄막
        {
            int bulletCountA = 36;
            int bulletCountB = 18;
            int bulletCount = checkCount % 2 == 0 ? bulletCountA : bulletCountB;
            for (int index = 0; index < bulletCount; index++)
            {
                GameObject clone = null;
                var b = prefab.GetComponent<BulletBase>();
                if (b != null)
                {
                    var bullet = BulletPool.Instance.RentBullet(b.type, transform.position, transform.rotation);
                    if (bullet != null)
                        clone = bullet.gameObject;
                }
                if (!clone)
                    clone = Instantiate(prefab, transform.position, transform.rotation);

                if (clone)
                {
                    clone.transform.localScale *= scale;
                    clone.transform.Translate(position, Space.Self);

                    if (parent) clone.transform.parent = parent;

                    if (overrideCollisionSettings.useTheseSettings) _OverrideCollisionSettings(clone);

                    if (prefabSmartOverrides) _SmartOverrides(clone);

                    clone.SetActive(true);

                    var bulletBase = clone.GetComponent<BulletBase>();
                    if (bulletBase)
                    {
                        var angle = Mathf.PI * 2 * index / bulletCount;
                        clone.transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);
                        bulletBase.velocity = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * speed;
                    }
                }
            }
            checkCount++;
        }
        else if (mode == Settings.Mode.Dinamic)
        {

        }
        audioProperties.Play();
    }

    void _OverrideCollisionSettings(GameObject clone)
    {
        var scoreBase = clone.GetComponent<IScoreBase>();
        if (scoreBase != null)
        {
            scoreBase.structuralIntegrity = overrideCollisionSettings.structuralIntegrity;
        }
    }
    void _SmartOverrides(GameObject clone)
    {
        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_spriteRenderer)
        {
            var orderInLayer = _spriteRenderer.sortingOrder;
            _spriteRenderer = clone.GetComponent<SpriteRenderer>();
            if (_spriteRenderer != null) _spriteRenderer.sortingOrder = orderInLayer + 1;
        }

        var scoreBase = clone.GetComponent<IScoreBase>();
        if (scoreBase != null) scoreBase.friend = gameObject;
    }

    SpriteRenderer _spriteRenderer;
}