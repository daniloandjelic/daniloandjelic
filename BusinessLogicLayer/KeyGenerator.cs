using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class KeyGenerator<T> where T : class
    {
        private KeyGenerator<T> m_KeyGenerator;
        public KeyGenerator<T> Instance
        {
            get
            {
                if (m_KeyGenerator == null)
                    m_KeyGenerator = new KeyGenerator<T>();

                return m_KeyGenerator; 
            }
        }

        IGenericDataAccessLayer<T> m_Dal = null;

        private long GenerateKey(IGenericDataAccessLayer<T> dal)
        {
            m_Dal = dal;
            return m_Dal.GetAll().Max(f => (long)f.GetType().GetProperty("Id").GetValue(f)) + 1;
        }

        

    }
}
