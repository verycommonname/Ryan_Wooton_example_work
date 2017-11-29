from robot_vars import *
import numpy as np
from hasimpy import *
import matplotlib.pyplot as plt
import matplotlib.animation as animation
from phase_calculation import computePhase
from feature_calculation import featureCalc
from target_value_calculation import targetCalc, getParameterVec, getTrajectory
import cma

# Created by Ryan Wooton - WOO13394001
#
# Function - This program implements machine learning to optimise performance.

def getTrajectories(wArray):
    c = np.linspace(0, 1, 10)  # 30
    h = 0.01

    zValue, time = computePhase()

    psiNorm = featureCalc(zValue, c, h)

    trajectory = np.zeros((2, 100))

    # wArray = np.zeros((y.shape[1], 50)) # 30
    wArray = wArray.reshape((2, 10))

    g = np.zeros((2, 1))
    a = 1
    b = 1
    dt = time[1] - time[0]

    for i in range(0, wArray.shape[0]):
        #        plt.plot(w)

        #        fTargetPredicted = psiNorm.dot(w)
        #        plt.plot(fTargetPredicted, 'g')

        trajectory[i, :] = getTrajectory(psiNorm, wArray[i, :], g[i], a, b, 0, dt)

    # Above the function works the same way as the previous stage's main function.

    length1 = 1
    length2 = 1
    # Here the program sets the leg length.

    y = np.zeros((2, 100)) # This set the size of the co-ordinate array
    y[0, :] = length1 * np.cos(trajectory[0, :]) + length2 * np.cos(trajectory[0, :] + trajectory[1, :])
    y[1, :] = length1 * np.sin(trajectory[0, :]) + length2 * np.sin(trajectory[0, :] + trajectory[1, :])
    # Above the x and y co-ordinates are calculated.

    return trajectory, y, dt

def getCost(wArray):

    trajectory, y, dt = getTrajectories(wArray) # Gets the trajectories from the previous function

    # For target calculation
    velocity = np.diff(trajectory) / dt
    velocity = np.hstack((np.zeros((2, 1)), velocity))
    # Calculates the velocity by dividing the trajectory difference by the time differential.

    acceleration = np.diff(velocity) / dt
    acceleration = np.hstack((acceleration, np.zeros((2, 1))))
    # Calculates the acceleration by dividing the velocity difference by the time differential.

    targetPosition = np.array([1, 1]) # This sets the target for the reinforcement program to move towards.

    cost = np.sum((targetPosition-y[:, 50])**2) + 10**-5 * np.sum(acceleration**2)
    # Here the cost for the action is calculated and returned.

    return cost


if __name__ == "__main__":

    getCost(np.ones((20,1)))

    print("hello")

    opts = cma.CMAOptions()
    opts.set('maxiter', 200) # Here the program initiates the iteration limiter.
    es = cma.CMAEvolutionStrategy(np.ones((20,)), 0.5, opts)
    # Here the program initialises the evolution strategy using the initial location, the initial sigma value and the
    # iteration limiter.

    #res = cma.fmin(getCost, np.ones((20,)), 0.1, options=opts)
    meanCosts = []
    iteration = 1
    # Here the program initialises the mean cost array and the iteration number.
    while not es.stop(): # loops for each iteration.
        solutions = es.ask() # Calls the evolution strategy.
        costsForSolutions = [getCost(s) for s in solutions] # Computes the cost for the current iteration.
        es.tell(solutions, costsForSolutions)
        es.disp() # passes the function values for the next iteratinon and display them.
        meanCosts.append(np.mean(costsForSolutions)) # Adds the calculated mean to the storage array.
        iteration = iteration + 1 # Adds one to the iteration.

    es.result_pretty() # Prints the iteration log summary

    trajectory, y, dt = getTrajectories(solutions[0]) # Gets the optimised summary.

    plt.figure()
    plt.xlabel('Co-ordinate number')
    plt.ylabel('Co-ordinate value')
    plt.title('Co-ordinate chart')
    plt.plot(y[0, :], 'b--', label='longitude position')
    plt.plot(y[1, :], 'g', label='Latitude position')
    plt.legend()
    plt.show()
    # Prints a chart showing the optimised co-ordinate values.

    plt.figure()
    plt.xlabel('Iteration')
    plt.ylabel('Function Value')
    plt.title('Learning curve for [1,1], 10**-5, 0.5') # 0.5
    plt.plot(np.array(meanCosts))
    # Prints the learning curve


    print("hello")