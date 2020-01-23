﻿namespace System
{
    public sealed class Converter
    {
        private readonly Action<object> log;

        public Converter() { }

        public Converter(Action<object> log)
        {
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public bool TryConvert(object obj, out bool result)
        {
            try
            {
                result = Convert.ToBoolean(obj);
                return true;
            }
            catch (Exception ex)
            {
                this.log?.Invoke(ex);

                result = default;
                return false;
            }
        }

        public bool TryConvert(object obj, out byte result)
        {
            try
            {
                result = Convert.ToByte(obj);
                return true;
            }
            catch (Exception ex)
            {
                this.log?.Invoke(ex);

                result = default;
                return false;
            }
        }

        public bool TryConvert(object obj, out sbyte result)
        {
            try
            {
                result = Convert.ToSByte(obj);
                return true;
            }
            catch (Exception ex)
            {
                this.log?.Invoke(ex);

                result = default;
                return false;
            }
        }

        public bool TryConvert(object obj, out char result)
        {
            try
            {
                result = Convert.ToChar(obj);
                return true;
            }
            catch (Exception ex)
            {
                this.log?.Invoke(ex);

                result = default;
                return false;
            }
        }

        public bool TryConvert(object obj, out short result)
        {
            try
            {
                result = Convert.ToInt16(obj);
                return true;
            }
            catch (Exception ex)
            {
                this.log?.Invoke(ex);

                result = default;
                return false;
            }
        }

        public bool TryConvert(object obj, out ushort result)
        {
            try
            {
                result = Convert.ToUInt16(obj);
                return true;
            }
            catch (Exception ex)
            {
                this.log?.Invoke(ex);

                result = default;
                return false;
            }
        }

        public bool TryConvert(object obj, out int result)
        {
            try
            {
                result = Convert.ToInt32(obj);
                return true;
            }
            catch (Exception ex)
            {
                this.log?.Invoke(ex);

                result = default;
                return false;
            }
        }

        public bool TryConvert(object obj, out uint result)
        {
            try
            {
                result = Convert.ToUInt32(obj);
                return true;
            }
            catch (Exception ex)
            {
                this.log?.Invoke(ex);

                result = default;
                return false;
            }
        }

        public bool TryConvert(object obj, out long result)
        {
            try
            {
                result = Convert.ToInt64(obj);
                return true;
            }
            catch (Exception ex)
            {
                this.log?.Invoke(ex);

                result = default;
                return false;
            }
        }

        public bool TryConvert(object obj, out ulong result)
        {
            try
            {
                result = Convert.ToUInt64(obj);
                return true;
            }
            catch (Exception ex)
            {
                this.log?.Invoke(ex);

                result = default;
                return false;
            }
        }

        public bool TryConvert(object obj, out float result)
        {
            try
            {
                result = Convert.ToSingle(obj);
                return true;
            }
            catch (Exception ex)
            {
                this.log?.Invoke(ex);

                result = default;
                return false;
            }
        }

        public bool TryConvert(object obj, out double result)
        {
            try
            {
                result = Convert.ToDouble(obj);
                return true;
            }
            catch (Exception ex)
            {
                this.log?.Invoke(ex);

                result = default;
                return false;
            }
        }

        public bool TryConvert(object obj, out decimal result)
        {
            try
            {
                result = Convert.ToDecimal(obj);
                return true;
            }
            catch (Exception ex)
            {
                this.log?.Invoke(ex);

                result = default;
                return false;
            }
        }

        public bool TryConvert(object obj, out string result)
        {
            try
            {
                result = Convert.ToString(obj);
                return true;
            }
            catch (Exception ex)
            {
                this.log?.Invoke(ex);

                result = string.Empty;
                return false;
            }
        }

        public bool TryConvert(object obj, out DateTime result)
        {
            try
            {
                result = Convert.ToDateTime(obj);
                return true;
            }
            catch (Exception ex)
            {
                this.log?.Invoke(ex);

                result = default;
                return false;
            }
        }
    }
}