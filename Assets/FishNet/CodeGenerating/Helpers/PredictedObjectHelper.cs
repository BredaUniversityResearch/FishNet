﻿using FishNet.Component.Prediction;
using MonoFN.Cecil;
using System;
using System.Reflection;

namespace FishNet.CodeGenerating.Helping
{
    internal class PredictedObjectHelper
    {
        #region Reflection references.
        //Names.
        internal string FullName;
        //Prediction.
        internal MethodReference SendRigidbodyStatesInternal_MethodRef;
        internal MethodReference InstantiatedRigidbodyCountInternal_Get_MethodRef;
        #endregion

        #region Const.
        internal const uint MAX_RPC_ALLOWANCE = ushort.MaxValue;
        internal const string AWAKE_METHOD_NAME = "Awake";
        internal const string DISABLE_LOGGING_TEXT = "This message may be disabled by setting the Logging field in your attribute to LoggingType.Off";
        #endregion

        internal bool ImportReferences()
        {
            Type predictedObjectType = typeof(PredictedObject);
            FullName = predictedObjectType.FullName;

            //If the same module then do not proceed. These are not used within the same module.
            if (predictedObjectType.Module.Assembly.FullName == CodegenSession.Module.Assembly.FullName)
                return true;

            foreach (MethodInfo mi in predictedObjectType.GetMethods((BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)))
            {
                if (mi.Name == nameof(PredictedObject.SendRigidbodyStatesInternal))
                { 
                    SendRigidbodyStatesInternal_MethodRef = CodegenSession.ImportReference(mi);
                    break;
                }
            }

            foreach (PropertyInfo pi in predictedObjectType.GetProperties())
            {
                if (pi.Name == nameof(PredictedObject.InstantiatedRigidbodyCountInternal))
                {
                    InstantiatedRigidbodyCountInternal_Get_MethodRef = CodegenSession.ImportReference(pi.GetMethod);
                    break;
                }
            }

            return true;
        }
    }
}