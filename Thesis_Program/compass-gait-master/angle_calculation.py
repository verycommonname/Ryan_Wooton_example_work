from robot_vars import *
import numpy as np
from hasimpy import *
import matplotlib.pyplot as plt
import matplotlib.animation as animation
from phase_calculation import computePhase
from feature_calculation import featureCalc, c, h
from robot_vars import g
from run_compass_gait import *

sin = np.sin
cos = np.cos
tan = np.tan
pi = np.pi

def coOrdCalc(startPoint, midPoint, endPoint):
	lengthArray = np.zeros((3,))
	coOrdCount = np.zeros((3,))
	
	p1length = np.sqrt((((midPoint[1] - startPoint[1])**2)+((midPoint[2] - startPoint[2])**2)))
	
	p2length = np.sqrt((((endPoint[1] - midPoint[1])**2)+((endPoint[2] - midPoint[2])**2)))

startPoint = point0(hst, hsw)
midPoint = point3(hst, hsw)
endPoint = point4(hst, hsw)


