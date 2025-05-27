using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyChat
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Windows Vista(6.0) 이상에서 DPI 인식 모드 설정
            if (Environment.OSVersion.Version.Major >= 6)
            {
                // 프로세스를 Per-Monitor DPI Aware로 설정하여 고해상도(4K) 환경에서 UI가 선명하게 보이도록 함
                SetProcessDpiAwarenessContext(DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);  
            }
            // 애플리케이션에 비주얼 스타일 적용 (모던 UI)
            Application.EnableVisualStyles();
            // 텍스트 렌더링을 GDI+ 기반으로 설정 (호환성)
            Application.SetCompatibleTextRenderingDefault(false);
            // 메인 폼(P2PChat) 실행
            Application.Run(new P2PChat());
        }

        // user32.dll의 SetProcessDpiAwarenessContext 함수 선언 (DPI 인식 모드 설정용)
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDpiAwarenessContext(IntPtr dpiFlag);

        // Per-Monitor DPI Aware V2 모드에 해당하는 상수 값
        private static readonly IntPtr DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2 = new IntPtr(-4);
    }
}
