using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softv.Providers
{
    public class ProviderSoftv
    {
        public static UsuarioProvider Usuario
        {
            get { return UsuarioProvider.Instance; }
        }
        public static RoleProvider Role
        {
            get { return RoleProvider.Instance; }
        }
        public static PermisoProvider Permiso
        {
            get { return PermisoProvider.Instance; }
        }
        public static ModuleProvider Module
        {
            get { return ModuleProvider.Instance; }
        }
        public static SecutityProvider Secutity
        {
            get { return SecutityProvider.Instance; }
        }

        public static SessionProvider Session
        {
            get { return SessionProvider.Instance; }
        }

        public static UsuarioSystemProvider UsuarioSystem
        {
            get { return UsuarioSystemProvider.Instance; }
        }

        public static TerminalProvider Terminal
        {
            get { return TerminalProvider.Instance; }
        }




    }
}
