using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Proximus.Tests
{
    [TestClass]
    public class OpenStreetMapsTests
    {
        [TestMethod]
        public void Test()
        {
            Location l1 = new Location() { Lat = 40.7505, Lng = -73.9936 };
            Location l2 = new Location() { Lat = 40.6429, Lng = -73.7794 };

            Mock<OpenStreetMaps> mock = new Mock<OpenStreetMaps>();

            var result = OpenStreetMaps.Instance.GetDistance(l1, l2);
        }



        public string JsonResponse()
        {
            return "{\\\"hints\\\":{\\\"visited_nodes.average\\\":\\\"314.0\\\",\\\"visited_nodes.sum\\\":\\\"314\\\"},\\\"info\\\":{\\\"copyrights\\\":[\\\"GraphHopper\\\",\\\"OpenStreetMap contributors\\\"],\\\"took\\\":6},\\\"paths\\\":[{\\\"distance\\\":27884.444,\\\"weight\\\":1310.535208,\\\"time\\\":1310473,\\\"transfers\\\":0,\\\"points_encoded\\\":true,\\\"bbox\\\":[-73.994845,40.643137,-73.780102,40.753478],\\\"points\\\":\\\"c~uwFp}rbMeAfD{RoM|Msb@xViw@jDyK`AgDRq@Nu@VoC`@aBA_BE]SaAc@aAa@c@mIqF]WGKGcBNoHf`@_u@fBsEzBcSJeBCoFDmCN}C?w@OgBYqBMiBAq@@oADm@nCaVRaBTsA\\\\\\\\uA`@}A`I{Vj@mB`@cBXyAl@qER{BT_EPuBz@gHhCoXrCqVVeB^gB^{Ah@iBr@mBb@cAd@{@n@eAbIaLx@oAv@yAt@aBj@_Bb@gBzEuWtBqM`@iBf@aBj@_Br@yAnHiNfB}Dp@mBj@mBb@wB\\\\\\\\uBRyBd@gJJgAP{EdBmb@B{BCwAIsBOoBWoB_@wBe@kBg@yAo@}AcFeKm@yA[_Ai@wB[yAYiBS{Di@gPIsBQmBYsB_@kBg@iBmC}Ic@_BcJec@mKof@eFcWuAiHgCiMw@sD]_CyAwN_@oCi@yCs@aEiCwKe@yAg@iBSgA?_@Ba@Ja@Ri@r@}Af@u@b@[h@M|B[f@C|Da@dAWdAa@hDaAzB}@vBiAjCcBfAy@hAcAfAgApHeIjBaBjB{ApBwArBmA`DwAhEgBjG_CxBaAxBkArBoArBuAzDqCvGoFnDgDlCmCz@aAfA_BX[n@iAd@oA\\\\\\\\uAn@eE~AwIVkA\\\\\\\\iAj@mArAmBpAsAlB{Ax@k@vC}AbCoBt@u@|@eAxAiBzByCpA}A`A}@bBsA|AgAhDsBjOoHzDaBbVyIdIaDvDcBdFaCnLgGtF_Cxm@yYfBw@rb@ePpNmFzQcHxAg@|A_@xCg@nAM|AGzHIvIPtFXdCZpFfArCz@xBx@~@f@v@h@t@n@`EzDrGpFtBhBvAdAfAp@rCpApAb@vA`@rAXhAPtBPxADvA@zDOfBWvBa@zAe@jBs@tBeAxG}DzFcE|BkBnCaCpCmCxDcE~@kAz@qAhAuBZu@Xw@p@uBl@oCVgBRiBNkBDiB@kBCyAG_B[yJCyBDuEb@mQKcEJiBX[PKpDqAZURg@Li@A]Ca@Qg@kA}C_@gAa@y@]g@i@aAa@eAOk@w@qEU_AYw@M{A@_@FQLKT@JLf@~A\\\",\\\"instructions\\\":[{\\\"distance\\\":80.573,\\\"heading\\\":299.02,\\\"sign\\\":0,\\\"interval\\\":[0,1],\\\"text\\\":\\\"Continue onto West 31st Street\\\",\\\"time\\\":8287,\\\"street_name\\\":\\\"West 31st Street\\\"},{\\\"distance\\\":404.415,\\\"sign\\\":2,\\\"interval\\\":[1,2],\\\"text\\\":\\\"Turn right onto 8th Avenue\\\",\\\"time\\\":41595,\\\"street_name\\\":\\\"8th Avenue\\\"},{\\\"distance\\\":1695.606,\\\"sign\\\":2,\\\"interval\\\":[2,6],\\\"text\\\":\\\"Turn right onto West 36th Street\\\",\\\"time\\\":101729,\\\"street_name\\\":\\\"West 36th Street\\\"},{\\\"distance\\\":8375.392,\\\"sign\\\":7,\\\"interval\\\":[6,94],\\\"text\\\":\\\"Keep right\\\",\\\"time\\\":402596,\\\"street_name\\\":\\\"\\\"},{\\\"distance\\\":2654.468,\\\"sign\\\":0,\\\"interval\\\":[94,110],\\\"text\\\":\\\"Continue onto Long Island Expressway, I 495\\\",\\\"time\\\":136508,\\\"street_name\\\":\\\"Long Island Expressway, I 495\\\"},{\\\"distance\\\":488.477,\\\"sign\\\":-7,\\\"interval\\\":[110,115],\\\"text\\\":\\\"Keep left onto Long Island Expressway, I 495\\\",\\\"time\\\":25120,\\\"street_name\\\":\\\"Long Island Expressway, I 495\\\"},{\\\"distance\\\":318.601,\\\"sign\\\":7,\\\"interval\\\":[115,118],\\\"text\\\":\\\"Keep right\\\",\\\"time\\\":16384,\\\"street_name\\\":\\\"\\\"},{\\\"distance\\\":2845.704,\\\"sign\\\":7,\\\"interval\\\":[118,158],\\\"text\\\":\\\"Keep right\\\",\\\"time\\\":111038,\\\"street_name\\\":\\\"\\\"},{\\\"distance\\\":227.302,\\\"sign\\\":7,\\\"interval\\\":[158,163],\\\"text\\\":\\\"Keep right\\\",\\\"time\\\":11689,\\\"street_name\\\":\\\"\\\"},{\\\"distance\\\":9324.831,\\\"sign\\\":-7,\\\"interval\\\":[163,249],\\\"text\\\":\\\"Keep left\\\",\\\"time\\\":344805,\\\"street_name\\\":\\\"\\\"},{\\\"distance\\\":211.148,\\\"sign\\\":-7,\\\"interval\\\":[249,251],\\\"text\\\":\\\"Keep left\\\",\\\"time\\\":10859,\\\"street_name\\\":\\\"\\\"},{\\\"distance\\\":468.513,\\\"sign\\\":7,\\\"interval\\\":[251,255],\\\"text\\\":\\\"Keep right\\\",\\\"time\\\":24094,\\\"street_name\\\":\\\"\\\"},{\\\"distance\\\":651.175,\\\"sign\\\":7,\\\"interval\\\":[255,274],\\\"text\\\":\\\"Keep right\\\",\\\"time\\\":59183,\\\"street_name\\\":\\\"\\\"},{\\\"distance\\\":39.674,\\\"sign\\\":7,\\\"interval\\\":[274,275],\\\"text\\\":\\\"Keep right\\\",\\\"time\\\":4760,\\\"street_name\\\":\\\"\\\"},{\\\"distance\\\":31.399,\\\"sign\\\":7,\\\"interval\\\":[275,278],\\\"text\\\":\\\"Keep right\\\",\\\"time\\\":3767,\\\"street_name\\\":\\\"\\\"},{\\\"distance\\\":67.166,\\\"sign\\\":1,\\\"interval\\\":[278,281],\\\"text\\\":\\\"Turn slight right\\\",\\\"time\\\":8059,\\\"street_name\\\":\\\"\\\"},{\\\"distance\\\":0.0,\\\"sign\\\":4,\\\"last_heading\\\":241.64495009642474,\\\"interval\\\":[281,281],\\\"text\\\":\\\"Arrive at destination\\\",\\\"time\\\":0,\\\"street_name\\\":\\\"\\\"}],\\\"legs\\\":[],\\\"details\\\":{},\\\"ascend\\\":0.0,\\\"descend\\\":0.0,\\\"snapped_waypoints\\\":\\\"c~uwFp}rbM|pSkth@\\\"}]}";
        }
    }
}
