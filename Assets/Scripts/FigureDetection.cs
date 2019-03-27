using System.Collections;
using System.Collections.Generic;

public interface IFigureDetection {

    void setPoint(Coordinate point);
    void setPoint(IFlyingObject flyingObject);
    List<Figure> detection();
}
