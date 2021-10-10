using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Auth
{
    public static class ApiKeyAuthenticationHandler
    {
        private const string _tenantClaimKey = "https://omnitro.com/tenantId";

        private static readonly Dictionary<Guid, string> _tenantIdApiKey = new Dictionary<Guid, string>
        {
            { new Guid("98F5F123-8FEE-4FE2-8AE7-D461C82AC5D6"), "5pbypqjFN83MZkfbxdRkPswZ57GTw37X" }, //ashleys
            { new Guid("91DB5266-16C7-448A-8A36-01E70AB68D25"), "tnxkvERgbaj3uFC25284A4hVtfaezSzW" }, //Ekkanano
            { new Guid("E3BC50BA-019D-11EB-ADC1-0242AC120002"), "QMpjwGGFdxSEvFCGaeA4Wvy5MqsvbXMX" }, //Oplog Demo
            { new Guid("AD48780E-1CC0-40AF-95A9-066864EDE53B"), "CAtc44wVd6e6t2Lck9ZS4eFLn9aa8XTF" }, //Kityboof
            { new Guid("7909509B-1BA0-45C8-86DD-081DF420931B"), "9YKGYyt7Qnezg8gqfN5QJYEK59vnqQ3t" }, //ArmorArt
            { new Guid("72051C3B-ABDE-4CE6-ABCD-1778523835C7"), "DmtdvZpgSNK59dqf9hdkpjExnV7STAnz" }, //NTimeMemories
            { new Guid("28BE60DD-ACA7-4D80-A96B-1C6AE18C2324"), "haS8qLE5mDuyJVHgxW2CsTnqASjEvguH" }, //Fertelli
            { new Guid("C9B17C88-9D8E-4253-A82B-1D00D150E2D4"), "PfJGNcmcXw9SF3K89ZrQKnsjGWrxUdDd" }, //MesBısous
            { new Guid("88D8B4DB-2F48-4AA0-9C88-1D0444C7ABC7"), "Gct65pUgknjuF2xqN9UTSp7Z6pGMDvMe" }, //InovatifGlobal
            { new Guid("2677E0C8-0859-4A94-8BBB-1EF41BFDDD15"), "4wnm7XU9nV4SPvCvNn4YtFDXpRUP8UkA" }, //LocosMocos
            { new Guid("179429C9-8DC3-4959-9DC5-20ACF7B8E8B6"), "zaFxHWwcPBMUqu3fQWpU8ensp8Z2rUQA" }, //SwissSmile
            { new Guid("CDC108A2-8854-4950-AE5A-2436414AA1EF"), "37RR8R9mfmwLKYx9c2HwabvhQ24unFBx" }, //webdenal
            { new Guid("57A796BC-75BA-4026-82E2-26910319D341"), "G56wEt6nJ7NGk5TsWrG6D9jqqPNrDHZu" }, //Apero
            { new Guid("083C386C-B3E1-45B1-A04C-27C460091803"), "5VRkB2qceRphV6CubpvL3acbqvBb7QZt" }, //Madamecoco
            { new Guid("424DB183-5928-4814-B07F-2A305FB59405"), "aFDV5F3hJwdv4F6cWXaQ2aRmQ7L9nqMR" }, //Fulique
            { new Guid("F1970505-DC61-4C3A-A4A5-2A7A8C489C90"), "th6JFnCEw4AdqpR72PW9AeYu4PMBZpDh" }, //Ekonsat
            { new Guid("511AE425-EB16-4B70-8D5A-34028CB3D4B8"), "jjgG7WcjLXKEBv86Zh3jybvGteXejmnT" }, //Kitopet
            { new Guid("6F7AA100-68B5-40DD-B86C-34CCE51219C9"), "CnLcSB2a9zzA3tjjbCtjZknFBy55L9Dt" }, //milagron
            { new Guid("90816290-5DA1-42D3-93AF-34FD2791C514"), "RrVExZakNkyrmjjU5PDnDzCMEWk57EmQ" }, //G-Besin
            { new Guid("6DEDBAE6-542D-4440-9771-3B0C286A7BFA"), "Xt8PTEcA8vHTGrHEDu7Ev94jD3Cf9LWv" }, //Sernes
            { new Guid("C86A5BFB-8E03-4077-BD56-3D6BDFEB5521"), "5Z3a2kP4Ck85MPasHewfSUdjupzVEncR" }, //Amnesty
            { new Guid("6F336067-862D-466A-A8E6-4A01AC4D09D7"), "uySE72WLhZVHX5MhGDFFvtL6Xa4k54ke" }, //IlgiStore
            { new Guid("F6BB4C09-61A8-48C0-AFC1-4C520AB0133A"), "8ZeyVjErV6AD5KHSUPLw6vGDrHRVP3vA" }, //WoohooBox
            { new Guid("ABF7D33A-08FA-44E6-9F55-4EBAD5C70DED"), "GeEdAhWZJxeSvRe8jeVDkRZ9fA8EgzWK" }, //BabyAria
            { new Guid("6B85870C-0314-457A-96AE-53BEFD1A7DB0"), "t6DdfDYvkbqYHPtLZuT5uwkEZPsTUEZ3" }, //vakumama
            { new Guid("18D826B2-BCDC-4459-843B-55BD0E72BC4D"), "wA54VuzXtjF8SR6LUtxQD2nzx5VyquqX" }, //PalmHouseLiving
            { new Guid("8EDFDB53-B87B-4CD8-BE9B-589A302029B6"), "3rnZDXj9EkxrBxKcg9ZQANtWQYNAcCNr" }, //Placeboriginals
            { new Guid("7F55AC42-023D-45C5-8943-5ABB591946AE"), "TFMs4qa8ZNWqUM7FefApP8SggZTdjxS5" }, //Nasaqu
            { new Guid("FB98A5B3-CB60-4C8D-939B-5D49EF044323"), "GZZW6BrXtExKn22BSBpykFCnrU3KFeUV" }, //Safinur
            { new Guid("0EADDF71-EB2D-4889-BBEB-6175556EBF9A"), "u6fgT7A2eYc3gzmP55UKVCkGDrh4A3Xg" }, //Nutraline
            { new Guid("4F0CB310-EF8A-4DEC-8289-627805BDD7AD"), "jVPZ76bGPZKqfgup75yr7w2vnypjM2Aa" }, //MakiSandals
            { new Guid("45FFF386-2045-4805-B1BD-634E81617EA4"), "rrWPpN6vCZx7Ff2YPRxKUvCrPya2wKxA" }, //galen
            { new Guid("10177216-0D0E-4BF0-B385-64ABCC1F777E"), "684EKE5DsnaN9Yftj4S9aPk43uaGMjnu" }, //Evecare
            { new Guid("30CA997B-6B84-4A00-A3E7-66832A0C07D8"), "AD7qQhAU9QGMDRHG3uAtC3gCaXxazr8K" }, //NishGroup
            { new Guid("559DDFEE-7462-43C9-9BA7-6A6B881B33EA"), "22yfSa3Pw4FuNQcty5KLMqVVFadfnFys" }, //TumuTumu
            { new Guid("F608C20C-48FA-42B2-B169-74B69E746CA7"), "4Dqbn3SK8pJ7QNmuEjxUVUN8yfszra9H" }, //NeoToys
            { new Guid("BCA6AC6A-86D5-4B3E-9AA1-75A26412C131"), "z52rsJcbUSAdTXdWfnXcpG39672NGsns" }, //TheMagger
            { new Guid("98E99339-C168-4F91-BA39-7E3FBE1767B4"), "JBJ9aKUcrUBr3VpxvCGmasauWfDk7Ver" }, //InsideOut
            { new Guid("CC5F7AE8-38AB-43D4-BD48-81814F649704"), "xeQ2dPMqFtR8SLJ4grkysDCbeArKPf6k" }, //Fonfique
            { new Guid("BA68C1D1-B2DA-48E5-93BF-822B223B1A2E"), "pWQtuTNfe3KYeWFw3JJYWz9QPG44KzPh" }, //Anafortis
            { new Guid("8C9124B4-D2E5-42C0-9E09-8B0452536C42"), "LL2Hn9nc2rHHKQny23k8ZFEevuMkSSaK" }, //Mamalattes
            { new Guid("6FAAF4A0-D698-40F7-A8F4-955CDB4181DC"), "ZnvvbpdKxEEbX4C96pXJJ57TCxfRc4Gy" }, //Toucheprive
            { new Guid("EE2EB373-9154-4F24-A3A7-9A13F36818E6"), "ggp9x67LDLXgFcmuNej5Xd3VeEn47yvN" }, //Kizilay
            { new Guid("8EFC5888-008D-4518-BB09-9A8FD3008EC9"), "JVQBsRRPUu75cq2bJCXA9RQBcSwpETuY" }, //Beauty
            { new Guid("8AFDEA82-65EC-4BCD-A2C9-9B29137A392D"), "CmyfAgTty36uAMbdw2F95qzP8MdV8AYm" }, //ByPinkfreud
            { new Guid("3A953B28-90A6-4A6A-8819-A1972F4F4DAF"), "HQh2mNkmdVv63J2wsarhbLfnSgFbEtXx" }, //TRTmarket
            { new Guid("ADF12006-5091-460C-B23D-A603B8259C2E"), "PNgmSeyu942EcfvxbBJvrVzpNrWgdexT" }, //Mugo
            { new Guid("8C1FAB88-EDCD-4874-B765-B009AF63196A"), "EMQFFjzmhgB8WCQL5cAk4pxK2cBD4aNs" }, //RocknDogs
            { new Guid("D6335B81-8D03-4578-922F-B32ECEEA1F62"), "uGbhXHf6xtLLsDZyVHHC9rRBk9s2ZctF" }, //CBCfashion
            { new Guid("DDE3551D-A4E4-4A29-8F07-B52B57AB02CF"), "NgYr6R79ZrfcRsCLHyZmSP7Kc7nmDA2a" }, //BagisiklikDestek
            { new Guid("888F4603-34A7-495B-839B-BBE07A90320E"), "pr6QNppV2Ner4haMCUXk7phZ6SccJkgd" }, //OPLOG Sample
            { new Guid("1537E6C8-F299-4F25-82A5-BC826DA482E0"), "whbqm5WwM4mzaJDa7VfFb34QGDNNX4wq" }, //E-Canta
            { new Guid("361BAB32-E3CF-4B46-9AC8-C6F36DB83D4A"), "ejUsKMe2CHBfvbERxgVTscsaU95TMHmj" }, //iamnotbasic
            { new Guid("E09BE040-2BF7-4511-A9C5-C916E2CACA0F"), "ft9adDLxkQaE9bVKSxCjzp34MFFKSLdB" }, //Popdog
            { new Guid("287863AC-8468-452D-A2CD-D1797C3B40A7"), "aHvyXqADqvcTu6KXMYwp98ajCMM2zAXr" }, //ReflectStudio
            { new Guid("7440AAEF-A5AB-41BA-816F-D29FBF54815F"), "Vn6znHBX3zgn8Hh9FLYt3u6TSqcAscw4" }, //Lucky&Friends
            { new Guid("961CB962-AC64-4035-A18E-DA555C309B3B"), "4XhKtV68QrjEQdq9fLTNfBE8nkrXCKEy" }, //Fantilator
            { new Guid("FB36D181-D168-433D-810A-DFDD73E32034"), "PuBxUTcWe62rCjQ9PxNhUHbt9shYfk9v" }, //komiliolive
            { new Guid("DF40DC70-6404-4699-AC21-E2106F0297BB"), "ZLM69ZHAWRkvF7c3aea6EcdPTzvhcsnY" }, //ontrailstore
            { new Guid("DE398C71-4D7B-4C7C-B410-E49B0C3C1CFD"), "a7HHu9rnamcnFcyjCCsVG63UYg5WYZxv" }, //Lincsquad
            { new Guid("79558294-EEC0-4623-BDF5-E592330A32E1"), "3bHPsZBfkkRZbBRVxtVpMLD68B9v5hFs" }, //CanKozmetik
            { new Guid("8F8ACB0D-8B46-4C05-83B4-E857627EF117"), "xQ6JhMVpa2Yw66ZRWtSXb2y6JEfAH9mZ" }, //mycurli
            { new Guid("02850991-F565-425B-98DC-E85D06F496DE"), "Ps22kLRFgmVWhWfT8cDwR4uc4r7kZCRp" }, //rossmann
            { new Guid("FA393070-DB8E-4B16-9090-EC76789B4562"), "DMk89CHCRnjTRtQtpcSsmg2VQ9sbnMy9" }, //Aposto
            { new Guid("D50AC5D9-60B9-48A9-AA6B-EF6FEAD400FA"), "2k44czhzrZFMgELYvHBVYECyEtNpjzzZ" }, //Lindosnaturals
            { new Guid("3EDE2B7D-C2CE-4768-86CF-EF90B24A2DA4"), "NGSXv7vdyB5abU3X8CNvRPUfuvFgNd4c" }, //PUYABetterLife
            { new Guid("89CD7906-8B5B-4008-9BAF-F18F326CA5A7"), "VXUjQKwRDvk56Ej4huVDgYRry2XfKhDX" }, //florii
            { new Guid("DDC2D7E0-E067-4166-832F-F26C9D4CB568"), "8eHuecRdDQaD7MjFKsFvtnNSvMLwJzu2" }, //suco
            { new Guid("CF4B2CA7-1D0F-499F-8173-F576F1869FFC"), "3gWvTK8NUuGV2r6vcpcfXvnuHYz6e7sm" }, //bizevdeyokuz
            { new Guid("CF1C793E-5A8C-4888-AD3F-F6805431B172"), "nHKWMXGaemrHj3r8pYugskFenktScdXB" }, //eczastok
            { new Guid("638DC713-F46D-4120-8325-F7B5A6BB4F7B"), "9sNyzYYnU6mLaHG23rJChJz7MmVzQwFD" }, //deneme
            { new Guid("386FB6D3-873D-4EBA-999B-FCFB59080DA7"), "HGhj8LkLGJUtCDfpqks7pNGf3GYBWs3k" }, //Topshop
            { new Guid("C612B496-F984-4474-AA35-FD8934C18FE0"), "wPGwAq5C6dNZc7fVnDNLgUYRLphkPGq7" }  //Belitha
        };



        public static async Task HandleAuthenticateAsync(HttpContext context, Func<Task> next)
        {
            if (!context.Request.Headers["Authorization"].ToString().Contains("Bearer"))
            {
                if (context.Request.Headers["X-Tenant-Id"].Any())
                {
                    var tenantId = new Guid(context.Request.Headers["X-Tenant-Id"].FirstOrDefault());

                    var isValid = IsValidRequest(context.Request.Headers["Authorization"].ToString(), tenantId);
                    if (!isValid)
                    {
                        throw new UnauthorizedAccessException();
                    }

                    // https://leastprivilege.com/2012/09/24/claimsidentity-isauthenticated-and-authenticationtype-in-net-4-5/
                    var identity = new ClaimsIdentity("ApiKey");
                    identity.AddClaim(new Claim(_tenantClaimKey, tenantId.ToString()));

                    var currentPrincipal = new ClaimsPrincipal(identity);
                    context.User = currentPrincipal;

                    if (context.User.Claims == null || !context.User.Claims.Any())
                    {
                        throw new UnauthorizedAccessException();
                    }

                    await next();
                }
                else
                {
                    await next();
                }
            }
            else
            {
                await next();
            }
        }

        private static bool IsValidRequest(string authorizationParameterValue, Guid tenantId)
        {
            return _tenantIdApiKey.Any(x => x.Value == authorizationParameterValue && x.Key.Equals(tenantId));
        }
    }
}
