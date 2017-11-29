A = imread('Imgtex.tiff'); % load the initial image
figure
imshow(A) % displays the initial image

% Here the size of the image is calculated with the size of the image being
% stored by the number of rows and columns it takes up.
imageSize = size(A);
numRows = imageSize(1);
numCols = imageSize(2);

% Below the program calculates the wavelength to be used later in the
% program by first calculating the minimum wavelength using a set
% calculation and a maximum wavelength by finding the hypotenuse of the
% number of rows and columns and uses these to calculate the wavelength of
% the image.
minimumWave = 4/sqrt(2);
maximumWave = hypot(numRows,numCols);
n = floor(log2(maximumWave/minimumWave));
actualWave = 2.^(0:(n-2)) * minimumWave;

% This section of code calculates what degree the image is currently at, by
% creating a vector that counts from the first value to the last value
% using the middle value as middle value as the amount to count up in.
deltaVal = 45;
orientation = 0:deltaVal:(180-deltaVal);

% Here the program generates a gabor filter using the wavelength and the
% orientation of the image.
gaborVal = gabor(actualWave,orientation);

gaborMag = imgaborfilt(A,gaborVal);
% Here the program extracts the gobor magnitude features from the original
% image.

for i = 1:length(gaborVal)% A loop that goes through the entire gabor 
                          % filter
    sigma = 0.5*gaborVal(i).Wavelength; % Here the program generates a 
                                        % sigma value for the individual
                                        % gabor filter.
    smoothingVal = 3; % This variable modifies how most smoothing takes place
    gaborMag(:,:,i) = imgaussfilt(gaborMag(:,:,i),smoothingVal*sigma); 
    % Above the program generates the gabor magnitude matrix.
end

X = 1:numCols;
Y = 1:numRows;
[X,Y] = meshgrid(X,Y);
% Above the program produces the x and y co-ordinates for each pixel in the
% image, while below, the program links the x co-ordinates to the Gabor 
% magnitude before linking the y co-ordinates onto it as well.
featureMatrix = cat(3,gaborMag,X);
featureMatrix = cat(3,featureMatrix,Y);

% Here the program calculates the total number of pixels before reshaping
% the X matrix to better fit a k-mean clustering model.
pixelsCount = numRows*numCols;
X = reshape(featureMatrix,pixelsCount,[]);

% Here the program normalises the matrix as to make each element have be a
% zero mean unit variance.
X = bsxfun(@minus, X, mean(X));
X = bsxfun(@rdivide,X,std(X));

% Using principle component analysis on each entry, the program transforms 
%the matrix into a intensity image which is then diplayed.
coeffVal = pca(X);
intensityImage = reshape(X*coeffVal(:,1),numRows,numCols);
figure
imshow(intensityImage,[])

%Below the program repeats the k-means clustering process five times too
%avoid confusion when search the means of each pixel, these values are then
%used to segment the image into however many segments are specified, the
%program then visualises the results and prints them to the screen.
segmentiedImage = kmeans(X,20,'Replicates',5);
segmentiedImage = reshape(segmentiedImage,[numRows numCols]);
figure
imshow(label2rgb(segmentiedImage))