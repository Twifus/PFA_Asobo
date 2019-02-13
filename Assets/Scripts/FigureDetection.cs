using System.Collections;
using System.Collections.Generic;

public interface IFigureDetection {

    /*bool analyzeLoop();
    bool analyzeBarrel();
    bool analyzeCubanEight();*/

    void setPoint(Coordinate point);
    List<Figure> detection();
}
