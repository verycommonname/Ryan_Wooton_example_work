A = imread('Imgtex.tiff');
% Here the image is read into the program.
for rotateVal = 0:3 % Creates a for loop that loops four times.
    rotate = imrotate(A,rotateVal*45); % Each iteration of the loop rotates
                                       % the image by 45 degrees.
    for columnNum = 1:10 % Creates a loop that loops ten times.
        stats = graycomatrix(rotate, 'offset', [0 columnNum*2],'NumLevels', 256, 'Symmetric',true);
        % Above the program creates a grey level co-occurance using the
        % image at it's current rotation and reading it with a number of
        % different parameters, including how offset the next pixel to the
        % side is compared to its neighbour(using twice the current loop
        % iteration number), the number of grey levels and the current
        % ordering of the values.
        
        trueMatrixStats((rotateVal*10) + columnNum) = graycoprops(stats,{'Contrast','Correlation','Energy','Homogeneity'});
        % Above the program stores a matrix of information regarding
        % the previously generated grey level co-occurance, recording
        % statistics such as contrast and correlation.
    end
end

% Below the program repeats what was done above, however with a slight
% variance in that the 'Symmetric' variable within the 'graycomatrix'
% function is set to false.
for rotateVal = 0:3
    rotate = imrotate(A,rotateVal*45);
    for columnNum = 1:10
        stats = graycomatrix(rotate, 'offset', [0 columnNum*2],'NumLevels', 256, 'Symmetric',false);
        
        falseMatrixStats((rotateVal*10) + columnNum) = graycoprops(stats,{'Contrast','Correlation','Energy','Homogeneity'});
        
    end
end