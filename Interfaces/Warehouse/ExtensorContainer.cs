﻿using ColossalFramework;
using ColossalFramework.Threading;
using ICities;
using Klyte.Commons.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Klyte.Commons.Interfaces
{
    public sealed class ExtensorContainer : Singleton<ExtensorContainer>, ISerializableDataExtension
    {
        public static event Action OnDataLoaded;

        public Dictionary<Type, IDataExtensor> Instances { get; private set; } = new Dictionary<Type, IDataExtensor>();

        #region Serialization
        public IManagers Managers => SerializableDataManager?.managers;

        public ISerializableData SerializableDataManager { get; private set; }

        public void OnCreated(ISerializableData serializableData) => SerializableDataManager = serializableData;
        public void OnLoadData()
        {
            LogUtils.DoLog($"LOADING DATA {GetType()}");
            instance.Instances = new Dictionary<Type, IDataExtensor>();
            List<Type> instancesExt = ReflectionUtils.GetInterfaceImplementations(typeof(IDataExtensor), GetType());
            LogUtils.DoLog($"SUBTYPE COUNT: {instancesExt.Count}");
            foreach (Type type in instancesExt)
            {
                LogUtils.DoLog($"LOADING DATA TYPE {type}");
                var basicInstance = (IDataExtensor)type.GetConstructor(new Type[0]).Invoke(new Type[0]);
                if (!SerializableDataManager.EnumerateData().Contains(basicInstance.SaveId))
                {
                    LogUtils.DoLog($"NO DATA TYPE {type}");
                    instance.Instances[type] = basicInstance;
                    basicInstance.LoadDefaults();
                    continue;
                }
                using var memoryStream = new MemoryStream(SerializableDataManager.LoadData(basicInstance.SaveId));
                byte[] storage = memoryStream.ToArray();
                try
                {
                    instance.Instances[type] = basicInstance.Deserialize(type, storage) ?? basicInstance;
                    if (CommonProperties.DebugMode)
                    {
                        string content = System.Text.Encoding.UTF8.GetString(storage);
                        LogUtils.DoLog($"{type} DATA {storage.Length}b => {content}");
                    }
                }
                catch (Exception e)
                {
                    string content = System.Text.Encoding.UTF8.GetString(storage);
                    LogUtils.DoLog($"{type} CORRUPTED DATA! => \nException: {e.Message}\n{e.StackTrace}\nData  {storage.Length}b:\n{content}");
                    instance.Instances[type] = null;
                }
            }

            ThreadHelper.dispatcher.Dispatch(() =>
            {
                OnDataLoaded?.Invoke();
                OnDataLoaded = null;
            });
        }

        // Token: 0x0600003B RID: 59 RVA: 0x00004020 File Offset: 0x00002220
        public void OnSaveData()
        {
            LogUtils.DoLog($"SAVING DATA {GetType()}");

            foreach (Type type in instance.Instances.Keys)
            {
                if (instance.Instances[type]?.SaveId == null || Singleton<ToolManager>.instance.m_properties.m_mode != ItemClass.Availability.Game)
                {
                    continue;
                }


                byte[] data = instance.Instances[type]?.Serialize();
                if (CommonProperties.DebugMode)
                {
                    string content = System.Text.Encoding.UTF8.GetString(data);
                    LogUtils.DoLog($"{type} DATA => {content}");
                }
                if (data.Length == 0)
                {
                    return;
                }
                try
                {
                    SerializableDataManager.SaveData(instance.Instances[type].SaveId, data);
                }
                catch (Exception e)
                {
                    LogUtils.DoErrorLog($"Exception trying to serialize {type}: {e} -  {e.Message}\n{e.StackTrace} ");
                }
            }
        }

        public void OnReleased()
        {
            foreach (IDataExtensor item in instance.Instances.Values)
            {
                item.OnReleased();
            }
        }
        #endregion
    }
}
