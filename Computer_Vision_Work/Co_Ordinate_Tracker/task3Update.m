function [initStateCur, initStateCovarCur] = task3Update(initState, initStateCovar, cartMod, obserNoise, c)
%Here the class load a number of variables from the main class, these
%variable include the state vector and the obersation noise and model
%amongst others, these variables are used to update the kalman filter.

innoCovar = cartMod * initStateCovar * cartMod' + obserNoise;
% To calculate the innovation covariance, the program multiplies the
% current and previous matrix of the obersation model by the covariance
% matrix, this value then has the obervation noise matrix added to it.
kalGain = initStateCovar * cartMod' * inv(innoCovar);
% By multiplying the covariance matrix of the state vector, the previous
% obversation model and the inverse of the proeviously calculated
% innovation covariance together, the program can calculate the kalman
% gain.
predObser = cartMod * initState;
% To calculate the predicted observation, the program multiplies the state
% vector and the observation model together.

initStateCur = initState + kalGain * (c - predObser);
% To calculate the estimated current state, the program first subtracts the
% predicted observation from the observation vector, the program then
% multplies this value by the previosly calculated kalman gain, finally the
% program adds the state vector.
initStateCovarCur = initStateCovar - kalGain * innoCovar * kalGain';
% Finally the program calculates the estimated covariance by first
% multiplying the current kalman gain, the previous kalman gain and the
% innovation cevariance together before subtracting this value from the
% covariance matrix of the state vector.
end