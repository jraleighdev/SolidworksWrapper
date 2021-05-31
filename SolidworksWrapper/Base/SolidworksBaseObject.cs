using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.Base
{
    /// <summary>
    /// Used for simple wrappers that only require one reference
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SolidworksBaseObject<T> : IDisposable
    {
        /// <summary>
        /// Com object that should be disposed
        /// </summary>
        private T _baseObject;
        
        #region Protected

        /// <summary>
        /// Reference to the com object 
        /// </summary>
        protected T BaseObject
        {
            get => _baseObject;
            set => _baseObject = value;
        }

        #endregion

        #region Public

        /// <summary>
        /// Public access to the com object 
        /// Need to manually dispose if used
        /// </summary>
        public T UnSafeObject => _baseObject;

        #endregion

        public SolidworksBaseObject(T comObject)
        {
            _baseObject = comObject;
        }

        public virtual void Dispose()
        {
            if (BaseObject == null)
            {
                return;
            }

            Marshal.ReleaseComObject(BaseObject);

            BaseObject = default;
        }
    }
}
