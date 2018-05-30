using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Net;
using System.Threading;
using System.IO;

namespace DrunkRally {
	public class WebSocketController : MonoBehaviour {
		public float axis_x = 0f;
		public float axis_y = 0f;
		public float handbrake = 0f;
        [SerializeField] private int httpPort;
        [SerializeField] private int wsPort;

		class WSInput {
			public float x;
			public float y;
			public float hb;
		}

		class WSControl : WebSocketBehavior
		{
			WebSocketController ctrl;

			public WSControl(WebSocketController controller) {
				this.ctrl = controller;
			}

			protected override void OnMessage (MessageEventArgs e)
			{
				WSInput input = JsonUtility.FromJson<WSInput>(e.Data);
				ctrl.axis_x = Mathf.Clamp(input.x, -1f, 1f);
				ctrl.axis_y = Mathf.Clamp(input.y / 7f, -1f, 1f);
				ctrl.handbrake = input.hb;
			}
		}

        class HTTPServer
        {
            IPAddress ip;
            int port;
            string responseString;

            public HTTPServer(IPAddress ip, int port, int wsPort) {
                this.ip = ip;
                this.port = port;
                string responseSetPort = "<script>ws_port = " + wsPort + "</script>";
                responseString = responseSetPort + File.ReadAllText("Assets/Standard Assets/DrunkRally/drunk_rally.html");
            }

            public void Start()
            {
                new Thread(() =>
                {
                    using (HttpListener listener = new HttpListener())
                    {
                        listener.Prefixes.Add("http://*:" + port + "/");
                        listener.Start();
                        while (true)
                        {
                            HttpListenerContext context = listener.GetContext();
                            HttpListenerResponse response = context.Response;
                            //string responseString = File.ReadAllText("sender.html");
                            //string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
                            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                            response.ContentLength64 = buffer.Length;
                            System.IO.Stream output = response.OutputStream;
                            output.Write(buffer, 0, buffer.Length);
                            output.Close();
                        }
                    }
                }).Start();
            }
        }

		void Awake() {
			var wsServer = new WebSocketServer (System.Net.IPAddress.Any, wsPort);
			wsServer.AddWebSocketService<WSControl> ("/control", () => new WSControl (this));
			wsServer.Start ();
            var httpServer = new HTTPServer(System.Net.IPAddress.Any, httpPort, wsPort);
            httpServer.Start();
		}
	}
}
