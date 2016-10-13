using System;
using System.Collections.Generic;
using ApiCaller;
using Nancy;
using System.Linq;

namespace PokeInfo
{
    public class PokeModule : NancyModule     
    {
        public PokeModule()
        {
            Get("/", async args =>   
            {
                string name = "";
                // object sprites = "";
                long weight = 0;
                long height = 0;
                long exp = 0;

                string url = "http://pokeapi.co/api/v2/pokemon/1";
                try
                {
                    url += (int)args.id;     
                }
                catch
                {
                    url += "151";    
                }

                await WebRequest.SendRequest(url, new Action<Dictionary<string, object>>( JsonResponse =>
                    {
                        name = (string)JsonResponse["name"];
                        // sprites = (object)JsonResponse["sprites"];
                        weight = (long)JsonResponse["weight"];
                        height = (long)JsonResponse["height"];
                        exp = (long)JsonResponse["base_experience"];
 
                        @ViewBag.name = name;
                        @ViewBag.weight = weight;
                        @ViewBag.height = height;
                        @ViewBag.exp = exp; 
                    }
                ));
                return View["poke"];
            }); 
        }
    }
}