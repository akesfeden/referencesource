// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// 
// ==--==
/*=============================================================================
**
** Class: MissingMemberException
**
**
** Purpose: The exception class for versioning problems with DLLS.
**
**
=============================================================================*/

namespace System {
    
    using System;
    using System.Runtime.Remoting;
    using System.Runtime.Serialization;
    using System.Runtime.CompilerServices;
    using System.Globalization;
        using System.Security.Permissions;
    using System.Runtime.Versioning;
    using System.Diagnostics.Contracts;
    
    [System.Runtime.InteropServices.ComVisible(true)]
    [Serializable] public class MissingMemberException : MemberAccessException, ISerializable {
        public MissingMemberException() 
            : base(Environment.GetResourceString("Arg_MissingMemberException")) {
            SetErrorCode(__HResults.COR_E_MISSINGMEMBER);
        }
    
        public MissingMemberException(String message) 
            : base(message) {
            SetErrorCode(__HResults.COR_E_MISSINGMEMBER);
        }
    
        public MissingMemberException(String message, Exception inner) 
            : base(message, inner) {
            SetErrorCode(__HResults.COR_E_MISSINGMEMBER);
        }

        protected MissingMemberException(SerializationInfo info, StreamingContext context) : base (info, context) {
            ClassName = (String)info.GetString("MMClassName");
            MemberName = (String)info.GetString("MMMemberName");
            Signature = (byte[])info.GetValue("MMSignature", typeof(byte[]));
        }
    
        public override String Message
        {
            [System.Security.SecuritySafeCritical]  // auto-generated
            get {
                if (ClassName == null) {
                    return base.Message;
                } else {
                    // do any desired fixups to classname here.
                    return Environment.GetResourceString("MissingMember_Name", 
                                                                       ClassName + "." + MemberName +
                                                                       (Signature != null ? " " + FormatSignature(Signature) : ""));
                }
            }
        }
    
        // Called to format signature
        [System.Security.SecurityCritical]  // auto-generated
        [ResourceExposure(ResourceScope.None)]
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        internal static extern String FormatSignature(byte [] signature);
    
    
    
        // Potentially called from the EE
        private MissingMemberException(String className, String memberName, byte[] signature)
        {
            ClassName   = className;
            MemberName  = memberName;
            Signature   = signature;
        }
    
        public MissingMemberException(String className, String memberName)
        {
            ClassName   = className;
            MemberName  = memberName;
        }
    
        [System.Security.SecurityCritical]  // auto-generated_required
        public override void GetObjectData(SerializationInfo info, StreamingContext context) {
            if (info==null) {
                throw new ArgumentNullException("info");
            }
            Contract.EndContractBlock();
            base.GetObjectData(info, context);
            info.AddValue("MMClassName", ClassName, typeof(String));
            info.AddValue("MMMemberName", MemberName, typeof(String));
            info.AddValue("MMSignature", Signature, typeof(byte[]));
        }
    
       
        // If ClassName != null, GetMessage will construct on the fly using it
        // and the other variables. This allows customization of the
        // format depending on the language environment.
        protected String  ClassName;
        protected String  MemberName;
        protected byte[]  Signature;
    }
}
