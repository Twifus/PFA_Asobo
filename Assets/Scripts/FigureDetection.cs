using System.Collections;
using System.Collections.Generic;

public interface IFigureDetection {

    void setPoint(IFlyingObject flyingObject);
    List<Figure> detection();
}
