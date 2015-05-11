using System.Text;

namespace Steganography
{
    /// <summary>
    ///     https://ru.wikipedia.org/wiki/RC4
    ///     ��������� ���� RC4 ��� ������ ��������� ��������, ����������� �������� �RSA Security�, � 1987 ����. ����������
    ///     �RC4� ���������� ���������� �Rivest cipher 4� ��� ����� ������� (�4� - ����� ������; ��. RC2, RC5, RC6; RC1
    ///     ������� �� ������������; RC3 ��������������, �� � ��� ���� ������� ����������), �� ��� ����� ������� ����������� ��
    ///     �Ron�s code� (���� ����)[2].
    ///     � ������� ���� ��� ���� ������� ������������ ������, � ������ �������� ��������� ��������������� ������ �����
    ///     ���������� ���������� � �������������, �� � �������� 1994 ���� ��� �������� ���� �������� ���������� � ������
    ///     �������� (����. mailing list) �Cypherpunks�[3]. ������ �������� RC4 ���� ������������ � ������ �������� usenet
    ///     �sci.crypt�. ������ �������� ��� ����� �� ��������� ������ � ���� ��������. �������������� �������� �� ������
    ///     ������� �����������, ����������� � �������������, ����������� ��������� RC4. ���������� ��������� ����� ���������
    ///     ���� RC4 ����������� ������������ ���������� ��� ��������� � ������������ � ��������� ���������.
    ///     ��������� ������ �������� ��������, �� ����� �� �������� ������������ ������. ������, �������� �RC4� ��������
    ///     �������� ������ �������� �RSA Security�. ����� �������� ��������� ��������� �� ������� ��������� �������� �����,
    ///     ���� ������ �������� �ARCFOUR� ��� �ARC4�, ���� � ���� ����. alleged RC4 � ��������������� RC4 (��������� �RSA
    ///     Security� ���������� �� ������������ ��������).
    ///     �������� ���������� RC4 ����������� � ��������� ������ ���������������� ���������� � ���������� ����������
    ///     (��������, WEP, WPA, SSL � TLS).
    ///     RC4 ���� ��������� ���������:
    ///     �������� ��� ���������� � ����������� ����������;
    ///     ������� �������� ������ ��������� � ����� �������.
    ///     � ��� ����� �����, ������������� ��� ������������� ������ ������, ����� 128 �����. ����������, ����������� �����
    ///     �SPA� (����. software publishers association) � �������������� ���, ��������� �������������� ����� RC4 � ������
    ///     ����� �� 40 ���. 56-� ������ ����� ��������� ������������ ����������� ���������� ������������ ��������[4].
    /// </summary>
    public class Arcfour
    {
        private int[] _key; // �������� �����

        public Arcfour()
        {
        }

        public Arcfour(string text)
        {
            SetKey(text);
        }

        /// <summary>
        ///     �������� ����� �������� ��� �key-scheduling algorithm� ��� �KSA�. ���� �������� ���������� ����, ���������� �� ����
        ///     �������������, ����������� � Key, � ������� ����� L ����. ������������� ���������� � ���������� ������� S, �����
        ///     ���� ������ �������������� ���� ������������, ������������ ������. ��� ��� ������ ���� �������� ����������� ��� S,
        ///     �� ������ ����������� �����������, ��� S ������ �������� ���� ����� �������� , ������� ��� ��� ��� ��������������
        ///     ������������� (S[i] := i).
        /// </summary>
        /// <param name="n"></param>
        /// <returns>������������</returns>
        public int[] Ksa(int n)
        {
            int l = _key.Length;
            int[] key = _key;
            var s = new int[n];
            for (int i = 0; i < n; i++) s[i] = i;
            for (int i = 0, j = 0; i < n; i++)
            {
                j = (j + s[i] + key[i%l])%n;
                int temp = s[i];
                s[i] = s[j];
                s[j] = temp;
            }
            return s;
        }

        /// <summary>
        ///     ��� ����� ��������� ���������� ����������� ��������������� ������������������ (����. pseudo-random generation
        ///     algorithm, PRGA). ��������� ��������� ������ RC4 ������������ ��������, ���������� � S. � ����� ����� RC4
        ///     ������������ ���� n-������ ����� K �� ��������� ������.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public byte[] Prga(int n)
        {
            int l = _key.Length;
            var k = new byte[n];
            int[] s = Ksa(256);
            for (int index = 0, i = 0, j = 0; index < n; index++)
            {
                i = (i + 1)%l;
                j = (j + s[i])%l;
                int temp = s[i];
                s[i] = s[j];
                s[j] = temp;
                int t = (s[i] + s[j])%l;
                k[index] = (byte) s[t];
            }
            return k;
        }

        public void SetKey(string keyText)
        {
            byte[] bytes = Encoding.Default.GetBytes(keyText);
            var key = new int[bytes.Length];
            for (int index = 0; index < bytes.Length; index++) key[index] = bytes[index];
            _key = key;
        }
    }
}