using UnityEngine;
using System.Collections;

public class BreakDown : MonoBehaviour
{
    ////linear: y=ax+c
    ////parabolic: y=ax^2+c
    ////sine: y=asin(bx-d)+c
    ////paraboloid: y/yr=x^2/xr^2+z^2/zr^2
    ////sphere: x^2/xr^2+y^2/yr^2+z^2/zr^2=r^2

    public float a, b, c, d, r, yr, xr, zr;

    public void EqnReader(string eq, string type)
    {
        if (type == "linear")
        {
            int i = 2;
            string atemp = "";
            string ctemp = "";
            bool neg = false;
            if (eq[i] == '-')
            {
                ++i;
                neg = true;
            }
            while (eq[i] != 'x')
            {
                atemp += eq[i];
                ++i;
            }
            if (neg)
            {
                a = -float.Parse(atemp);
            }
            else
            {
                a = float.Parse(atemp);
            }
            ++i;
            if (eq[i] == '-')
            {
                neg = true;
                ++i;
            }
            else if (eq[i] == '+')
            {
                neg = false;
                ++i;
            }
            while (i < eq.Length)
            {
                ctemp += eq[i];
                ++i;
            }
            if (neg)
            {
                c = -float.Parse(ctemp); ;
            }
            else
            {
                c = float.Parse(ctemp);
            }
        }
        else if (type == "parabolic")
        {
            int i = 2;
            string atemp = "";
            string ctemp = "";
            bool neg = false;
            if (eq[i] == '-')
            {
                neg = true;
                ++i;
            }
            while (eq[i] != 'x')
            {
                atemp += eq[i];
                ++i;
            }
            a = float.Parse(atemp);
            if (neg)
            {
                a = -a;
            }
            i += 3;
            if (eq[i] == '-')
            {
                neg = true;
                ++i;
            }
            else if (eq[i] == '+')
            {
                neg = false;
                ++i;
            }
            while (i < eq.Length)
            {
                ctemp += eq[i];
                ++i;
            }
            c = float.Parse(ctemp);
            if (neg)
            {
                c = -c;
            }
        }
        else if (type == "sine")
        {
            int i = 2;
            string atemp = "";
            string btemp = "";
            string ctemp = "";
            string dtemp = "";
            bool neg = false;
            if (eq[i] == '-')
            {
                neg = true;
                ++i;
            }
            while (eq[i] != 's')
            {
                atemp += eq[i];
                ++i;
            }
            a = float.Parse(atemp);
            if (neg)
            {
                a = -a;
            }
            i += 4;
            if (eq[i] == '-')
            {
                neg = true;
                ++i;
            }
            else
            {
                neg = false;
            }
            while (eq[i] != 'x')
            {
                btemp += eq[i];
                ++i;
            }
            b = float.Parse(btemp);
            if (neg) { b = -b; }
            ++i;
            if (eq[i] == '-')
            {
                neg = false;
                ++i;
            }
            else if (eq[i] == '+')
            {
                neg = true;
                ++i;
            }
            while (eq[i] != ')')
            {
                dtemp += eq[i];
                ++i;
            }
            d = float.Parse(dtemp);
            if (neg)
            {
                d = -d;
            }
            ++i;
            if (eq[i] == '-')
            {
                neg = true;
                ++i;
            }
            else
            {
                neg = false;
                ++i;
            }
            while (i < eq.Length)
            {
                ctemp += eq[i];
                ++i;
            }
            c = float.Parse(ctemp);
            if (neg) { c = -c; }

        }
        else if (type == "elliptic")
        {
            string yrtemp = "";
            string xrtemp = "";
            string zrtemp = "";
            int i = 2;
            bool neg = false;
            if (eq[i] == '-')
            {
                neg = true;
                ++i;
            }
            while (eq[i] != '=')
            {
                yrtemp += eq[i];
                ++i;
            }
            yr = float.Parse(yrtemp);
            if (neg) { yr = -yr; }
            i += 5;

            while (eq[i] != '^')
            {
                xrtemp += eq[i];
                ++i;
            }
            xr = float.Parse(xrtemp);
            i += 7;
            while (eq[i] != '^')
            {
                zrtemp += eq[i];
                ++i;
            }
            zr = float.Parse(zrtemp);
        }
        else if (type == "sphere")
        {
            string yrtemp = "";
            string xrtemp = "";
            string zrtemp = "";
            string rtemp = "";
            int i = 4;
            while (eq[i] != '^')
            {
                xrtemp += eq[i];
                ++i;
            }
            xr = float.Parse(xrtemp);
            i += 7;
            while (eq[i] != '^')
            {
                yrtemp += eq[i];
                ++i;
            }
            yr = float.Parse(yrtemp);
            i += 7;
            while (eq[i] != '^')
            {
                zrtemp += eq[i];
                ++i;
            }
            zr = float.Parse(zrtemp);
            i += 3;
            while (eq[i] != '^')
            {
                rtemp += eq[i];
                ++i;
            }
            r = float.Parse(rtemp);
        }

    }
}